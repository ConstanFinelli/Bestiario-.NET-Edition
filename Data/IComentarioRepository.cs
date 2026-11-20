using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IComentarioRepository
    {
        Task<IEnumerable<Comentario>> GetAllAsync();
        Task<IEnumerable<Comentario>> GetByBestiaAsync(Guid idBestia);
        Task<Comentario?> GetAsync(Guid id);
        Task AddAsync(Comentario comentario);
        Task<bool> UpdateAsync(Comentario comentario);
        Task<bool> DeleteAsync(Guid id);
    }
}
