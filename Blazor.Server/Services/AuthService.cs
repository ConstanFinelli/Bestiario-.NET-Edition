using DTOs;
using API.Clients;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public class AuthService
    {
        private readonly ProtectedSessionStorage _sessionStorage;

        public bool IsInitializing { get; private set; } = true;
        public bool IsAuthenticated => CurrentUser != null && (!Expiracion.HasValue || Expiracion.Value > DateTime.UtcNow);
        public UsuarioDTO? CurrentUser { get; private set; }
        public string? Token { get; private set; }
        public DateTime? Expiracion { get; private set; }
        public string? Username => CurrentUser?.Correo ?? _legacyUsername;
        public bool EsInvestigador => IsAuthenticated && (CurrentUser is InvestigadorDTO || CurrentUser?.TipoUsuario == "Investigador");
        public bool EsLector => IsAuthenticated && (CurrentUser is LectorDTO || CurrentUser?.TipoUsuario == "Lector");
        public bool EsInvitado => !IsAuthenticated;
        private string? _legacyUsername;
        private bool _isInitialized = false;

        public event Action? OnChange;

        public AuthService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            try
            {
                var tokenResult = await _sessionStorage.GetAsync<string>("auth_token");
                var expResult = await _sessionStorage.GetAsync<DateTime>("auth_exp");
                var userResult = await _sessionStorage.GetAsync<UsuarioDTO>("auth_user");

                if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value) &&
                    expResult.Success && expResult.Value > DateTime.UtcNow &&
                    userResult.Success && userResult.Value != null)
                {
                    Token = tokenResult.Value;
                    Expiracion = expResult.Value;
                    CurrentUser = userResult.Value;
                    _legacyUsername = CurrentUser.Correo;
                    BaseApiClient.SetAuthToken(Token);
                }
                else if (expResult.Success && expResult.Value <= DateTime.UtcNow)
                {
                    // Token expirado (>60 min)
                    await LogoutAsync();
                }
            }
            catch
            {
                // Manejo silencioso en caso de interop previo a renderizado
            }
            finally
            {
                IsInitializing = false;
                NotifyStateChanged();
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var authResult = await UsuarioApiClient.LoginAsync(username, password);
                if (authResult != null && !string.IsNullOrEmpty(authResult.Token))
                {
                    CurrentUser = authResult.Usuario;
                    Token = authResult.Token;
                    Expiracion = authResult.Expiracion;
                    _legacyUsername = authResult.Usuario.Correo;

                    BaseApiClient.SetAuthToken(Token);

                    try
                    {
                        await _sessionStorage.SetAsync("auth_token", Token);
                        await _sessionStorage.SetAsync("auth_exp", Expiracion.Value);
                        await _sessionStorage.SetAsync("auth_user", CurrentUser);
                    }
                    catch
                    {
                    }

                    IsInitializing = false;
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
                Expiracion = DateTime.UtcNow.AddMinutes(60);
                IsInitializing = false;
                NotifyStateChanged();
                return true;
            }

            return false;
        }

        public async Task<bool> RegistrarLectorAsync(string correo, string contrasenia, bool recibirNotificaciones = true)
        {
            var lectorDto = new LectorDTO
            {
                Correo = correo,
                Contrasenia = contrasenia,
                RecibirNotificaciones = recibirNotificaciones
            };

            var created = await UsuarioApiClient.AddLectorAsync(lectorDto);
            if (created != null && created.Id != Guid.Empty)
            {
                return await LoginAsync(correo, contrasenia);
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            CurrentUser = null;
            Token = null;
            Expiracion = null;
            _legacyUsername = null;
            BaseApiClient.ClearAuthToken();

            try
            {
                await _sessionStorage.DeleteAsync("auth_token");
                await _sessionStorage.DeleteAsync("auth_exp");
                await _sessionStorage.DeleteAsync("auth_user");
            }
            catch
            {
            }

            IsInitializing = false;
            NotifyStateChanged();
        }

        public void Logout()
        {
            _ = LogoutAsync();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
