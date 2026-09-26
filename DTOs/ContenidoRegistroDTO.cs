using System;

namespace DTOs
{
    public class ContenidoRegistroDTO
    {
        public Guid IdBestia { get; set; }
        public int NroRegistro { get; set; }
        public int NroContenido { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
    }
}
