using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetAsync(Guid id);
        Task<Usuario?> GetByCorreoAsync(string correo);
        Task AddAsync(Usuario usuario);
        Task<bool> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(Guid id);
        Task<Usuario?> ValidarCredencialesAsync(string correo, string contrasenia);
    }
}
