using System;

namespace Domain.Model
{
    public class Investigador : Usuario
    {
        public string Dni { get; private set; } = string.Empty;
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;

        public Investigador() : base() { }

        public Investigador(Guid id, string correo, string contrasenia, string dni, string nombre, string apellido, bool recibirNotificaciones = true)
            : base(id, correo, contrasenia, recibirNotificaciones)
        {
            SetDni(dni);
            SetNombre(nombre);
            SetApellido(apellido);
        }

        public Investigador(string correo, string contrasenia, string dni, string nombre, string apellido, bool recibirNotificaciones = true)
            : base(correo, contrasenia, recibirNotificaciones)
        {
            SetDni(dni);
            SetNombre(nombre);
            SetApellido(apellido);
        }

        public void SetDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException("El DNI no puede estar vacío");
            }
            Dni = dni.Trim();
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío");
            }
            Nombre = nombre.Trim();
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido no puede estar vacío");
            }
            Apellido = apellido.Trim();
        }
    }
}
