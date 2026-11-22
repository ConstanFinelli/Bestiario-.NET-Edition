using System;
using System.Collections.Generic;

namespace DTOs
{
    public class ReporteBestiasPorCategoriaDTO
    {
        public string Categoria { get; set; } = string.Empty;
        public int CantidadBestias { get; set; }
        public int Aprobadas { get; set; }
        public int Pendientes { get; set; }
    }

    public class ReporteBestiasPorPeligrosidadDTO
    {
        public string Peligrosidad { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int Aprobadas { get; set; }
        public int Pendientes { get; set; }
    }

    public class ReporteEstadisticasBestiasDTO
    {
        public int TotalBestias { get; set; }
        public int TotalAprobadas { get; set; }
        public int TotalPendientes { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalEvidencias { get; set; }
        public List<ReporteBestiasPorCategoriaDTO> PorCategoria { get; set; } = new();
        public List<ReporteBestiasPorPeligrosidadDTO> PorPeligrosidad { get; set; } = new();
    }

    public class ReporteRegistrosPorBestiaDTO
    {
        public Guid IdBestia { get; set; }
        public string BestiaNombre { get; set; } = string.Empty;
        public string Peligrosidad { get; set; } = string.Empty;
        public string HabitatNombre { get; set; } = string.Empty;
        public int NroRegistro { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string PublicadorCorreo { get; set; } = string.Empty;
        public string AprobadorNombre { get; set; } = string.Empty;
        public int CantidadContenidos { get; set; }
    }
}
