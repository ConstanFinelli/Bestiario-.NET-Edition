using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IBestiaRepository
    {
        Task<IEnumerable<Bestia>> GetAllAsync();
        Task<Bestia?> GetAsync(Guid id);
        Task AddAsync(Bestia bestia, IEnumerable<Guid>? categoriaIds = null);
        Task<bool> UpdateAsync(Bestia bestia, IEnumerable<Guid>? categoriaIds = null);
        Task<bool> DeleteAsync(Guid id);
    }
}
