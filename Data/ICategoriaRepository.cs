using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ICategoriaRepository
    {
        Task AddAsync(Categoria categoria);
        Task<bool> DeleteAsync(Guid id);
        Task<Categoria?> GetAsync(Guid id);
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<bool> UpdateAsync(Categoria categoria);
    }
}
