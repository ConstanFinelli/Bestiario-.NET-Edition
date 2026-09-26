using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO?> GetAsync(Guid id);
        Task<UsuarioDTO> AddInvestigadorAsync(InvestigadorDTO dto);
        Task<UsuarioDTO> AddLectorAsync(LectorDTO dto);
        Task<bool> UpdateAsync(UsuarioDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<UsuarioDTO?> ValidarCredencialesAsync(string correo, string contrasenia);
    }
}
