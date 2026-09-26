using System;
using System.Collections.Generic;

namespace DTOs
{
    public class BestiaDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Peligrosidad { get; set; } = string.Empty;
        public string Estado { get; set; } = "pendiente";

        public List<Guid> CategoriasIds { get; set; } = new List<Guid>();
        public List<string> CategoriasNombres { get; set; } = new List<string>();
    }
}
