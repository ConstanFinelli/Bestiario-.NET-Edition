using System;

namespace DTOs
{
    public class ComentarioDTO
    {
        public Guid Id { get; set; }
        public Guid IdBestia { get; set; }
        public string? BestiaNombre { get; set; }
        public Guid IdUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Contenido { get; set; } = string.Empty;
    }
}
