using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using DTOs;

namespace Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            this.reporteRepository = reporteRepository;
        }

        public async Task<ReporteEstadisticasBestiasDTO> GetEstadisticasBestiasAsync()
        {
            return await reporteRepository.GetEstadisticasBestiasAsync();
        }

        public async Task<IEnumerable<ReporteRegistrosPorBestiaDTO>> GetRegistrosPorBestiaAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string? estado = null,
            Guid? idBestia = null)
        {
            return await reporteRepository.GetRegistrosPorBestiaAsync(desde, hasta, estado, idBestia);
        }
    }
}
