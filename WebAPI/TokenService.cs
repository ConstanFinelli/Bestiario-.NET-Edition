using DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI
{
    public interface ITokenService
    {
        (string token, DateTime expiracion) GenerarToken(UsuarioDTO usuario);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public (string token, DateTime expiracion) GenerarToken(UsuarioDTO usuario)
        {
            var keyString = _config["Jwt:Key"] ?? "BestiarioClaveSecretaSuperSeguraParaFirmaJwt2026!#";
            var issuer = _config["Jwt:Issuer"] ?? "BestiarioAPI";
            var audience = _config["Jwt:Audience"] ?? "BestiarioClients";
            var expireMinutes = double.TryParse(_config["Jwt:ExpireMinutes"], out var m) ? m : 60;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(expireMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario),
                new Claim("tipo_usuario", usuario.TipoUsuario)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiracion,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(securityToken);

            return (tokenString, expiracion);
        }
    }
}
