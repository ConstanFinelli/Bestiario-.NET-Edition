using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IEvidenciaRepository
    {
        Task<IEnumerable<Evidencia>> GetByBestiaAsync(Guid idBestia);
        Task<Evidencia?> GetAsync(Guid idBestia, int nroEvidencia);
        Task<int> GetNextNroEvidenciaAsync(Guid idBestia);
        Task AddAsync(Evidencia evidencia);
        Task<bool> UpdateAsync(Evidencia evidencia);
        Task<bool> DeleteAsync(Guid idBestia, int nroEvidencia);
        Task<bool> AprobarAsync(Guid idBestia, int nroEvidencia);
    }
}

