using System;

namespace Domain.Model
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;

        // Los usuarios no requieren tener una colección de registros

        public Usuario() { }

        public Usuario(Guid id, string correo, string contrasenia)
        {
            Id = id;
            SetCorreo(correo);
            SetContrasenia(contrasenia);
        }

        public Usuario(string correo, string contrasenia)
        {
            Id = Guid.NewGuid();
            SetCorreo(correo);
            SetContrasenia(contrasenia);
        }

        public void SetCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                throw new ArgumentException("El correo no puede estar vacío");
            }
            Correo = correo.Trim();
        }

        public void SetContrasenia(string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(contrasenia))
            {
                throw new ArgumentException("La contraseña no puede estar vacía");
            }
            Contrasenia = contrasenia;
        }
    }
}
