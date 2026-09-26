using System;
using System.Collections.Generic;

namespace DTOs
{
    public class RegistroDTO
    {
        public Guid IdBestia { get; set; }
        public int NroRegistro { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public Guid IdUsuarioPublicador { get; set; }
        public string? CorreoPublicador { get; set; }
        public Guid? IdInvestigadorAprobador { get; set; }
        public string? NombreInvestigadorAprobador { get; set; }
        public string Estado { get; set; } = "pendiente";

        public List<ContenidoRegistroDTO> Contenidos { get; set; } = new List<ContenidoRegistroDTO>();
    }
}
