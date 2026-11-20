using System;

namespace DTOs
{
    public class CaracteristicaHabitatDTO
    {
        public Guid IdHabitat { get; set; }
        public int NroCaracteristica { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
