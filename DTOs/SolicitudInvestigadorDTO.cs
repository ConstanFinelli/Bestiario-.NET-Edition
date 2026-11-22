using System;

namespace DTOs
{
    public class SolicitudInvestigadorDTO
    {
        public Guid Id { get; set; }
        public Guid IdLector { get; set; }
        public string? CorreoLector { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "pendiente";
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public Guid? IdInvestigadorResolutor { get; set; }
        public string? NombreInvestigadorResolutor { get; set; }
    }

    public class CrearSolicitudInvestigadorDTO
    {
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
    }
}
