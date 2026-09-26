using System;

namespace Domain.Model
{
    public class Lector : Usuario
    {
        public Lector() : base() { }

        public Lector(Guid id, string correo, string contrasenia, bool recibirNotificaciones = true)
            : base(id, correo, contrasenia, recibirNotificaciones)
        {
        }

        public Lector(string correo, string contrasenia, bool recibirNotificaciones = true)
            : base(correo, contrasenia, recibirNotificaciones)
        {
        }
    }
}
