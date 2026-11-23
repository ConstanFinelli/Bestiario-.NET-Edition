using System.Threading.Tasks;

namespace Application.Services
{
    public interface IEmailService
    {
        Task EnviarCodigoRecuperacionAsync(string destinoCorreo, string codigo, int minutosExpiracion);
    }
}
