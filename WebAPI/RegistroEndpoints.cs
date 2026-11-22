using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class RegistroEndpoints
    {
        public static void MapRegistroEndpoints(this WebApplication app)
        {
            app.MapGet("/bestias/{idBestia}/registros", async (Guid idBestia, ClaimsPrincipal user, IRegistroService registroService) =>
            {
                var dtos = await registroService.GetByBestiaAsync(idBestia);
                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!esInvestigador)
                {
                    Guid? userId = Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : null;
                    dtos = dtos.Where(r =>
                        (r.Estado != null && r.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)) ||
                        (userId.HasValue && r.IdUsuarioPublicador == userId.Value))
                        .ToList();
                }
                return Results.Ok(dtos);
            })
            .WithName("GetRegistrosByBestia")
            .AllowAnonymous()
            .Produces<List<RegistroDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/bestias/{idBestia}/registros/{nroRegistro:int}", async (Guid idBestia, int nroRegistro, ClaimsPrincipal user, IRegistroService registroService) =>
            {
                var dto = await registroService.GetAsync(idBestia, nroRegistro);
                if (dto == null)
                    return Results.NotFound();

                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                Guid? userId = Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : null;
                bool esPropio = userId.HasValue && dto.IdUsuarioPublicador == userId.Value;

                if (!esInvestigador && !esPropio && !dto.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase))
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetRegistro")
            .AllowAnonymous()
            .Produces<RegistroDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/bestias/{idBestia}/registros", async (Guid idBestia, RegistroDTO dto, ClaimsPrincipal user, IRegistroService registroService) =>
            {
                try
                {
                    dto.IdBestia = idBestia;
                    bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                    if (Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var uid))
                    {
                        if (!esInvestigador || dto.IdUsuarioPublicador == Guid.Empty)
                        {
                            dto.IdUsuarioPublicador = uid;
                        }
                    }

                    if (!esInvestigador)
                    {
                        dto.Estado = "pendiente";
                        dto.IdInvestigadorAprobador = null;
                        dto.FechaAprobacion = null;
                    }

                    var result = await registroService.AddAsync(dto);
                    return Results.Created($"/bestias/{idBestia}/registros/{result.NroRegistro}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddRegistro")
            .RequireAuthorization()
            .Produces<RegistroDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPut("/bestias/{idBestia}/registros", async (Guid idBestia, RegistroDTO dto, IRegistroService registroService) =>
            {
                try
                {
                    dto.IdBestia = idBestia;
                    var found = await registroService.UpdateAsync(dto);
                    if (!found)
                        return Results.NotFound();

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateRegistro")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/bestias/{idBestia}/registros/{nroRegistro:int}", async (Guid idBestia, int nroRegistro, IRegistroService registroService) =>
            {
                var deleted = await registroService.DeleteAsync(idBestia, nroRegistro);
                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteRegistro")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPut("/bestias/{idBestia}/registros/{nroRegistro:int}/aprobar/{idInvestigador}", async (Guid idBestia, int nroRegistro, Guid idInvestigador, IRegistroService registroService) =>
            {
                var ok = await registroService.AprobarAsync(idBestia, nroRegistro, idInvestigador);
                if (!ok)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("AprobarRegistro")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
