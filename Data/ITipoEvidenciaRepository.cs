using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface ITipoEvidenciaRepository
    {
        Task<IEnumerable<TipoEvidencia>> GetAllAsync();
        Task<TipoEvidencia?> GetAsync(Guid id);
        Task AddAsync(TipoEvidencia tipoEvidencia);
        Task<bool> UpdateAsync(TipoEvidencia tipoEvidencia);
        Task<bool> DeleteAsync(Guid id);
    }
}

