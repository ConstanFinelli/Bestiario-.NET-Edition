using System;

namespace DTOs
{
    public class TipoEvidenciaDTO
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string ResourceType { get; set; } = string.Empty;
    }
}

