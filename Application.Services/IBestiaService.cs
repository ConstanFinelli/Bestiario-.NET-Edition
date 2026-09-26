using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IBestiaService
    {
        Task<BestiaDTO> AddAsync(BestiaDTO dto);
        Task<bool> UpdateAsync(BestiaDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<BestiaDTO?> GetAsync(Guid id);
        Task<IEnumerable<BestiaDTO>> GetAllAsync();
    }
}
