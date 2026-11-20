using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IComentarioService
    {
        Task<IEnumerable<ComentarioDTO>> GetAllAsync();
        Task<IEnumerable<ComentarioDTO>> GetByBestiaAsync(Guid idBestia);
        Task<ComentarioDTO?> GetAsync(Guid id);
        Task<ComentarioDTO> AddAsync(ComentarioDTO dto);
        Task<bool> UpdateAsync(ComentarioDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
