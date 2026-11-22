using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface ISolicitudInvestigadorService
    {
        Task<IEnumerable<SolicitudInvestigadorDTO>> GetAllAsync(string? estado = null);
        Task<IEnumerable<SolicitudInvestigadorDTO>> GetByLectorAsync(Guid idLector);
        Task<SolicitudInvestigadorDTO> CrearSolicitudAsync(Guid idLector, CrearSolicitudInvestigadorDTO dto);
        Task<bool> AprobarAsync(Guid idSolicitud, Guid idInvestigador);
        Task<bool> RechazarAsync(Guid idSolicitud, Guid idInvestigador);
    }
}
