using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data
{
    public interface ISolicitudInvestigadorRepository
    {
        Task<IEnumerable<SolicitudInvestigador>> GetAllAsync(string? estado = null);
        Task<IEnumerable<SolicitudInvestigador>> GetByLectorAsync(Guid idLector);
        Task<SolicitudInvestigador?> GetAsync(Guid id);
        Task<bool> HasPendienteAsync(Guid idLector);
        Task AddAsync(SolicitudInvestigador solicitud);
        Task<bool> AprobarAsync(Guid idSolicitud, Guid idInvestigador);
        Task<bool> RechazarAsync(Guid idSolicitud, Guid idInvestigador);
    }
}
