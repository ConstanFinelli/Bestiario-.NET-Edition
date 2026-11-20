using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class ComentarioEndpoints
    {
        public static void MapComentarioEndpoints(this WebApplication app)
        {
            app.MapGet("/comentarios", async (ClaimsPrincipal user, IComentarioService service) =>
            {
                var dtos = await service.GetAllAsync();
                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!esInvestigador)
                {
                    dtos = dtos.Where(c => c.Estado != null && c.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return Results.Ok(dtos);
            })
            .WithName("GetAllComentarios")
            .AllowAnonymous()
            .Produces<List<ComentarioDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/bestias/{idBestia}/comentarios", async (Guid idBestia, ClaimsPrincipal user, IComentarioService service) =>
            {
                var dtos = await service.GetByBestiaAsync(idBestia);
                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!esInvestigador)
                {
                    dtos = dtos.Where(c => c.Estado != null && c.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return Results.Ok(dtos);
            })
            .WithName("GetComentariosByBestia")
            .AllowAnonymous()
            .Produces<List<ComentarioDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/comentarios/{id}", async (Guid id, ClaimsPrincipal user, IComentarioService service) =>
            {
                var dto = await service.GetAsync(id);
                if (dto == null)
                    return Results.NotFound();

                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!esInvestigador && !dto.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase))
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetComentario")
            .AllowAnonymous()
            .Produces<ComentarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/comentarios", async (ComentarioDTO dto, ClaimsPrincipal user, IComentarioService service) =>
            {
                try
                {
                    if (dto.IdUsuario == Guid.Empty)
                    {
                        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        if (Guid.TryParse(userIdClaim, out Guid userId))
                        {
                            dto.IdUsuario = userId;
                        }
                    }

                    bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                    if (!esInvestigador)
                    {
                        dto.Estado = "pendiente";
                        dto.FechaAprobacion = null;
                    }

                    var result = await service.AddAsync(dto);
                    return Results.Created($"/comentarios/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddComentario")
            .RequireAuthorization()
            .Produces<ComentarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPut("/comentarios", async (ComentarioDTO dto, IComentarioService service) =>
            {
                try
                {
                    var found = await service.UpdateAsync(dto);
                    if (!found)
                        return Results.NotFound();

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateComentario")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/comentarios/{id}", async (Guid id, IComentarioService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteComentario")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPut("/comentarios/{id}/aprobar", async (Guid id, IComentarioService service) =>
            {
                var ok = await service.AprobarAsync(id);
                if (!ok)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("AprobarComentario")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}

