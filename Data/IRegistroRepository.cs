using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface IRegistroRepository
    {
        Task<IEnumerable<Registro>> GetByBestiaAsync(Guid idBestia);
        Task<Registro?> GetAsync(Guid idBestia, int nroRegistro);
        Task<int> GetNextNroRegistroAsync(Guid idBestia);
        Task AddAsync(Registro registro);
        Task<bool> UpdateAsync(Registro registro);
        Task<bool> DeleteAsync(Guid idBestia, int nroRegistro);
        Task<bool> AprobarAsync(Guid idBestia, int nroRegistro, Guid idInvestigador);
    }
}
