using System;

namespace DTOs
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string? Contrasenia { get; set; }
        public bool RecibirNotificaciones { get; set; } = true;
        public string TipoUsuario { get; set; } = "Usuario";
    }

    public class InvestigadorDTO : UsuarioDTO
    {
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        public InvestigadorDTO()
        {
            TipoUsuario = "Investigador";
        }
    }

    public class LectorDTO : UsuarioDTO
    {
        public LectorDTO()
        {
            TipoUsuario = "Lector";
        }
    }

    public class LoginRequestDTO
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
    }
}
