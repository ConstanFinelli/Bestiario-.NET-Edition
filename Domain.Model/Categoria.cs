using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Model
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        // public Collection<Bestia> Bestias { get; set; }

        public Categoria(Guid id, string nombre, string descripcion) {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public void SetNombre (string nombre){
            if (nombre.Length < 0)
            {
                throw new ArgumentException("El nombre no puede ser una cadena de carácteres vacíos");
            }
            Nombre = nombre;
        }

        public void SetDescripcion(string descripcion) 
        {
            if (descripcion.Length < 0)
            {
                throw new ArgumentException("La descripción no puede ser una cadena de carácteres vacíos");
            }
            Descripcion = descripcion;
        }

    }
}
