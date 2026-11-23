using System;

namespace Domain.Model
{
    public class TokenRecuperacion
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string CodigoHash { get; private set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public bool Usado { get; private set; }

        public Usuario? Usuario { get; set; }

        public TokenRecuperacion() { }

        public TokenRecuperacion(Guid idUsuario, string codigoHash, int minutosExpiracion = 15)
        {
            if (idUsuario == Guid.Empty)
            {
                throw new ArgumentException("El usuario es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(codigoHash))
            {
                throw new ArgumentException("El hash del código es obligatorio.");
            }
            if (minutosExpiracion <= 0)
            {
                throw new ArgumentException("Los minutos de expiración deben ser mayores a cero.");
            }

            Id = Guid.NewGuid();
            IdUsuario = idUsuario;
            CodigoHash = codigoHash.Trim();
            FechaCreacion = DateTime.UtcNow;
            FechaExpiracion = FechaCreacion.AddMinutes(minutosExpiracion);
            Usado = false;
        }

        public bool EstaVigente() => !Usado && DateTime.UtcNow <= FechaExpiracion;

        public void MarcarUsado() => Usado = true;
    }
}
