using System;

namespace DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
        public UsuarioDTO Usuario { get; set; } = new UsuarioDTO();
    }
}
