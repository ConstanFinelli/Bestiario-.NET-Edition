using System;
using System.Collections.Generic;

namespace DTOs
{
    public class HabitatDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Localizacion { get; set; } = string.Empty;
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public List<CaracteristicaHabitatDTO> Caracteristicas { get; set; } = new List<CaracteristicaHabitatDTO>();
    }
}
