using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _pass;
        private readonly string _from;

        public SmtpEmailService(IConfiguration configuration)
        {
            _host = GetSetting("SMTP_HOST", configuration["Smtp:Host"]) ?? "smtp.gmail.com";
            string? portStr = GetSetting("SMTP_PORT", configuration["Smtp:Port"]);
            _port = int.TryParse(portStr, out int p) ? p : 587;
            _user = GetSetting("SMTP_USER", configuration["Smtp:User"]) ?? string.Empty;
            _pass = GetSetting("SMTP_PASS", configuration["Smtp:Pass"]) ?? string.Empty;
            _from = GetSetting("SMTP_FROM", configuration["Smtp:From"]) ?? _user;
        }

        private static string? GetSetting(string envKey, string? configFallback)
        {
            string? envValue = Environment.GetEnvironmentVariable(envKey);
            return !string.IsNullOrWhiteSpace(envValue) ? envValue.Trim() : configFallback?.Trim();
        }

        public async Task EnviarCodigoRecuperacionAsync(string destinoCorreo, string codigo, int minutosExpiracion)
        {
            if (string.IsNullOrWhiteSpace(_user) || string.IsNullOrWhiteSpace(_pass))
            {
                throw new InvalidOperationException("El envío de correos SMTP no está configurado (SMTP_USER / SMTP_PASS).");
            }

            string remitente = string.IsNullOrWhiteSpace(_from) ? _user : _from;

            using var client = new SmtpClient(_host, _port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_user, _pass),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            string cuerpoHtml = $@"
<div style=""font-family: Arial, sans-serif; max-width: 520px; margin: 0 auto; border: 1px solid #d0d7de; border-radius: 6px; padding: 24px; background-color: #ffffff;"">
    <h2 style=""margin-top: 0; color: #0d6efd;"">Bestiario - Restablecimiento de Contraseña</h2>
    <p style=""color: #333333; font-size: 14px;"">
        Recibimos una solicitud para restablecer la contraseña de tu cuenta (<strong>{WebUtility.HtmlEncode(destinoCorreo)}</strong>).
    </p>
    <p style=""color: #333333; font-size: 14px;"">Tu código de verificación es:</p>
    <div style=""text-align: center; margin: 20px 0;"">
        <span style=""display: inline-block; font-size: 28px; font-weight: bold; letter-spacing: 6px; padding: 12px 24px; background-color: #f4f6f8; border: 1px solid #cfd8dc; border-radius: 4px; color: #111111;"">
            {WebUtility.HtmlEncode(codigo)}
        </span>
    </div>
    <p style=""color: #555555; font-size: 13px;"">
        Este código es válido por <strong>{minutosExpiracion} minutos</strong> y puede utilizarse una sola vez.
    </p>
    <p style=""color: #777777; font-size: 12px; margin-bottom: 0;"">
        Si no solicitaste este cambio, podés ignorar este correo de forma segura.
    </p>
</div>";

            using var message = new MailMessage(remitente, destinoCorreo)
            {
                Subject = "Bestiario - Código para restablecer tu contraseña",
                IsBodyHtml = true,
                Body = cuerpoHtml
            };

            try
            {
                await client.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                throw new InvalidOperationException($"No se pudo enviar el correo mediante SMTP ({_host}:{_port}): {ex.Message}", ex);
            }
        }
    }
}
