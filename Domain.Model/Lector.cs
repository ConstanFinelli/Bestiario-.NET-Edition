using System;

namespace Domain.Model
{
    public class Lector : Usuario
    {
        public Lector() : base() { }

        public Lector(Guid id, string correo, string contrasenia)
            : base(id, correo, contrasenia)
        {
        }

        public Lector(string correo, string contrasenia)
            : base(correo, contrasenia)
        {
        }
    }
}
