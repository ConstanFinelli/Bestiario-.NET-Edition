using System;

namespace Domain.Model
{
    public class CaracteristicaHabitat
    {
        public Guid IdHabitat { get; set; }
        public int NroCaracteristica { get; set; }
        public string Descripcion { get; private set; } = string.Empty;

        public Habitat? Habitat { get; set; }

        public CaracteristicaHabitat() { }

        public CaracteristicaHabitat(Guid idHabitat, int nroCaracteristica, string descripcion)
        {
            IdHabitat = idHabitat;
            NroCaracteristica = nroCaracteristica;
            SetDescripcion(descripcion);
        }

        public CaracteristicaHabitat(int nroCaracteristica, string descripcion)
        {
            NroCaracteristica = nroCaracteristica;
            SetDescripcion(descripcion);
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción de la característica no puede estar vacía");
            }
            Descripcion = descripcion.Trim();
        }
    }
}

