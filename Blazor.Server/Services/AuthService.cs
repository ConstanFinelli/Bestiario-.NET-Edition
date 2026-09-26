using DTOs;
using API.Clients;
using System;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public class AuthService
    {
        public bool IsAuthenticated => CurrentUser != null;
        public UsuarioDTO? CurrentUser { get; private set; }
        public string? Username => CurrentUser?.Correo ?? _legacyUsername;
        private string? _legacyUsername;

        public event Action? OnChange;

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var user = await UsuarioApiClient.LoginAsync(username, password);
                if (user != null)
                {
                    CurrentUser = user;
                    _legacyUsername = user.Correo;
                    NotifyStateChanged();
                    return true;
                }
            }
            catch
            {
                // Fallback si la API aún no inicializó o en credenciales fijas
            }

            if (username == "admin" && password == "password")
            {
                CurrentUser = new InvestigadorDTO
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Correo = "admin@bestiario.com",
                    Nombre = "Admin",
                    Apellido = "Investigador",
                    Dni = "12345678"
                };
                _legacyUsername = "admin";
                NotifyStateChanged();
                return true;
            }

            return false;
        }

        public bool Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                CurrentUser = new InvestigadorDTO
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Correo = "admin@bestiario.com",
                    Nombre = "Admin",
                    Apellido = "Investigador",
                    Dni = "12345678"
                };
                _legacyUsername = "admin";
                NotifyStateChanged();
                return true;
            }

            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
            _legacyUsername = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
