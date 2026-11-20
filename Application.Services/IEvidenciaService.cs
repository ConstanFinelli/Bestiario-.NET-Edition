using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IEvidenciaService
    {
        Task<IEnumerable<EvidenciaDTO>> GetByBestiaAsync(Guid idBestia);
        Task<EvidenciaDTO?> GetAsync(Guid idBestia, int nroEvidencia);
        Task<EvidenciaDTO> AddAsync(EvidenciaDTO dto);
        Task<bool> UpdateAsync(EvidenciaDTO dto);
        Task<bool> DeleteAsync(Guid idBestia, int nroEvidencia);
        Task<bool> AprobarAsync(Guid idBestia, int nroEvidencia);
    }
}

