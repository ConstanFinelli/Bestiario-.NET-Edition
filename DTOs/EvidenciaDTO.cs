using System;

namespace DTOs
{
    public class EvidenciaDTO
    {
        public Guid IdBestia { get; set; }
        public int NroEvidencia { get; set; }
        public string Estado { get; set; } = "pendiente";
        public DateTime FechaObtencion { get; set; } = DateTime.Now;
        public string FileId { get; set; } = string.Empty;
        public Guid IdTipoEvidencia { get; set; }
        public string? TipoEvidenciaDescripcion { get; set; }
        public string? TipoEvidenciaResourceType { get; set; }
    }
}
