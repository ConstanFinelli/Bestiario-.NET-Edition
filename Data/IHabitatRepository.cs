using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IHabitatRepository
    {
        Task<IEnumerable<Habitat>> GetAllAsync();
        Task<Habitat?> GetAsync(Guid id);
        Task AddAsync(Habitat habitat);
        Task<bool> UpdateAsync(Habitat habitat);
        Task<bool> DeleteAsync(Guid id);
    }
}

