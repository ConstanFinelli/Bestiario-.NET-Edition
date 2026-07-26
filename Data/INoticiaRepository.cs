using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface INoticiaRepository
    {
        Task AddAsync(Noticia noticia);
        Task<bool> DeleteAsync(Guid id);
        Task<Categoria?> GetAsync(Guid id);
        Task<IEnumerable<Noticia>> GetAllAsync();
        Task<bool> UpdateAsync(Noticia noticia);
    }
}
