using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IReporteService
    {
        Task<ReporteEstadisticasBestiasDTO> GetEstadisticasBestiasAsync();
        Task<IEnumerable<ReporteRegistrosPorBestiaDTO>> GetRegistrosPorBestiaAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string? estado = null,
            Guid? idBestia = null);
    }
}
