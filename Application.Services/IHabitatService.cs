using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IHabitatService
    {
        Task<IEnumerable<HabitatDTO>> GetAllAsync();
        Task<HabitatDTO?> GetAsync(Guid id);
        Task<HabitatDTO> AddAsync(HabitatDTO dto);
        Task<bool> UpdateAsync(HabitatDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

