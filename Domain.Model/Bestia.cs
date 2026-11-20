using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Bestia
    {
        public Guid Id { get; set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Peligrosidad { get; private set; } = string.Empty;
        public string Estado { get; private set; } = "pendiente"; // "pendiente" | "aprobado"
        public Guid? IdHabitat { get; set; }

        public Habitat? Habitat { get; set; }
        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
        public ICollection<Registro> Registros { get; set; } = new List<Registro>();
        public ICollection<Evidencia> Evidencias { get; set; } = new List<Evidencia>();

        public Bestia() { }

        public Bestia(Guid id, string nombre, string peligrosidad, string estado, Guid? idHabitat = null)
        {
            Id = id;
            SetNombre(nombre);
            SetPeligrosidad(peligrosidad);
            SetEstado(estado);
            IdHabitat = idHabitat;
        }

        public Bestia(string nombre, string peligrosidad, string estado = "pendiente", Guid? idHabitat = null)
        {
            Id = Guid.NewGuid();
            SetNombre(nombre);
            SetPeligrosidad(peligrosidad);
            SetEstado(estado);
            IdHabitat = idHabitat;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío");
            }
            Nombre = nombre.Trim();
        }

        public void SetPeligrosidad(string peligrosidad)
        {
            if (string.IsNullOrWhiteSpace(peligrosidad))
            {
                throw new ArgumentException("La peligrosidad no puede estar vacía");
            }
            Peligrosidad = peligrosidad.Trim();
        }

        public void SetEstado(string estado)
        {
            var est = estado?.Trim().ToLowerInvariant();
            if (est != "pendiente" && est != "aprobado")
            {
                throw new ArgumentException("El estado debe ser 'pendiente' o 'aprobado'");
            }
            Estado = est;
        }
    }
}
