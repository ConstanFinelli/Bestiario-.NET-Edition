using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface ITipoEvidenciaService
    {
        Task<IEnumerable<TipoEvidenciaDTO>> GetAllAsync();
        Task<TipoEvidenciaDTO?> GetAsync(Guid id);
        Task<TipoEvidenciaDTO> AddAsync(TipoEvidenciaDTO dto);
        Task<bool> UpdateAsync(TipoEvidenciaDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

