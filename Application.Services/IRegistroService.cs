using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IRegistroService
    {
        Task<IEnumerable<RegistroDTO>> GetByBestiaAsync(Guid idBestia);
        Task<RegistroDTO?> GetAsync(Guid idBestia, int nroRegistro);
        Task<RegistroDTO> AddAsync(RegistroDTO dto);
        Task<bool> UpdateAsync(RegistroDTO dto);
        Task<bool> DeleteAsync(Guid idBestia, int nroRegistro);
        Task<bool> AprobarAsync(Guid idBestia, int nroRegistro, Guid idInvestigador);
    }
}
