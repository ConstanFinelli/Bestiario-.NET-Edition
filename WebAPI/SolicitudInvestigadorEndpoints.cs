using System;
using System.Security.Claims;
using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class SolicitudInvestigadorEndpoints
    {
        public static void MapSolicitudInvestigadorEndpoints(this WebApplication app)
        {
            app.MapGet("/solicitudes-investigador", async (string? estado, ISolicitudInvestigadorService service) =>
            {
                var dtos = await service.GetAllAsync(estado);
                return Results.Ok(dtos);
            })
            .WithName("GetAllSolicitudesInvestigador")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces<List<SolicitudInvestigadorDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi();

            app.MapGet("/solicitudes-investigador/mias", async (ClaimsPrincipal user, ISolicitudInvestigadorService service) =>
            {
                if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                {
                    return Results.Unauthorized();
                }

                var dtos = await service.GetByLectorAsync(userId);
                return Results.Ok(dtos);
            })
            .WithName("GetMisSolicitudesInvestigador")
            .RequireAuthorization()
            .Produces<List<SolicitudInvestigadorDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPost("/solicitudes-investigador", async (CrearSolicitudInvestigadorDTO dto, ClaimsPrincipal user, ISolicitudInvestigadorService service) =>
            {
                if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                {
                    return Results.Unauthorized();
                }

                try
                {
                    var result = await service.CrearSolicitudAsync(userId, dto);
                    return Results.Created($"/solicitudes-investigador/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("CrearSolicitudInvestigador")
            .RequireAuthorization(policy => policy.RequireRole("Lector"))
            .Produces<SolicitudInvestigadorDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi();

            app.MapPut("/solicitudes-investigador/{id:guid}/aprobar", async (Guid id, ClaimsPrincipal user, ISolicitudInvestigadorService service) =>
            {
                if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var investigadorId))
                {
                    return Results.Unauthorized();
                }

                try
                {
                    var ok = await service.AprobarAsync(id, investigadorId);
                    if (!ok) return Results.NotFound();
                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AprobarSolicitudInvestigador")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPut("/solicitudes-investigador/{id:guid}/rechazar", async (Guid id, ClaimsPrincipal user, ISolicitudInvestigadorService service) =>
            {
                if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var investigadorId))
                {
                    return Results.Unauthorized();
                }

                try
                {
                    var ok = await service.RechazarAsync(id, investigadorId);
                    if (!ok) return Results.NotFound();
                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("RechazarSolicitudInvestigador")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
