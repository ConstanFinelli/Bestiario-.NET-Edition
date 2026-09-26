using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;

        public ICollection<Bestia> Bestias { get; set; } = new List<Bestia>();

        public Categoria() { }

        public Categoria(Guid id, string nombre, string descripcion)
        {
            Id = id;
            SetNombre(nombre);
            SetDescripcion(descripcion);
        }

        public Categoria(string nombre, string descripcion)
        {
            Id = Guid.NewGuid();
            SetNombre(nombre);
            SetDescripcion(descripcion);
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede ser una cadena de caracteres vacía");
            }
            Nombre = nombre.Trim();
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción no puede ser una cadena de caracteres vacía");
            }
            Descripcion = descripcion.Trim();
        }
    }
}
