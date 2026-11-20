using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    public class Habitat
    {
        public Guid Id { get; set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Localizacion { get; private set; } = string.Empty;
        public double Latitud { get; private set; }
        public double Longitud { get; private set; }

        public ICollection<CaracteristicaHabitat> Caracteristicas { get; set; } = new List<CaracteristicaHabitat>();
        public ICollection<Bestia> Bestias { get; set; } = new List<Bestia>();

        public Habitat() { }

        public Habitat(Guid id, string nombre, string localizacion, double latitud, double longitud)
        {
            Id = id;
            SetNombre(nombre);
            SetLocalizacion(localizacion);
            SetLatitud(latitud);
            SetLongitud(longitud);
        }

        public Habitat(string nombre, string localizacion, double latitud, double longitud)
        {
            Id = Guid.NewGuid();
            SetNombre(nombre);
            SetLocalizacion(localizacion);
            SetLatitud(latitud);
            SetLongitud(longitud);
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del hábitat no puede estar vacío");
            }
            Nombre = nombre.Trim();
        }

        public void SetLocalizacion(string localizacion)
        {
            if (string.IsNullOrWhiteSpace(localizacion))
            {
                throw new ArgumentException("La localización no puede estar vacía");
            }
            Localizacion = localizacion.Trim();
        }

        public void SetLatitud(double latitud)
        {
            if (latitud < -90 || latitud > 90)
            {
                throw new ArgumentException("La latitud debe estar entre -90 y 90");
            }
            Latitud = latitud;
        }

        public void SetLongitud(double longitud)
        {
            if (longitud < -180 || longitud > 180)
            {
                throw new ArgumentException("La longitud debe estar entre -180 y 180");
            }
            Longitud = longitud;
        }

        public void AgregarCaracteristica(string descripcion)
        {
            int siguienteNro = Caracteristicas.Count > 0 ? Caracteristicas.Max(c => c.NroCaracteristica) + 1 : 1;
            Caracteristicas.Add(new CaracteristicaHabitat(Id, siguienteNro, descripcion));
        }
    }
}
