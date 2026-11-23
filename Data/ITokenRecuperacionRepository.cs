using System;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface ITokenRecuperacionRepository
    {
        Task InvalidarPreviosAsync(Guid idUsuario);
        Task AddAsync(TokenRecuperacion token);
        Task<TokenRecuperacion?> GetVigenteAsync(Guid idUsuario, string codigoHash);
        Task MarcarUsadoAsync(Guid idToken);
    }
}
