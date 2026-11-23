using System;
using System.Threading.Tasks;
using API.Clients;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Blazor.Server.Services
{
    /// <summary>
    /// Mantiene el token JWT aislado por circuito de usuario en Blazor Server.
    /// </summary>
    public class AuthCircuitHandler : CircuitHandler
    {
        private readonly AuthService _authService;

        public AuthCircuitHandler(AuthService authService)
        {
            _authService = authService;
        }

        public override Func<CircuitInboundActivityContext, Task> CreateInboundActivityHandler(
            Func<CircuitInboundActivityContext, Task> next)
        {
            return async context =>
            {
                using (BaseApiClient.UseScopedToken(_authService.Token))
                {
                    await next(context);
                }
            };
        }
    }
}
