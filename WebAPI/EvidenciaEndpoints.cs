using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class EvidenciaEndpoints
    {
        public static void MapEvidenciaEndpoints(this WebApplication app)
        {
            app.MapGet("/bestias/{idBestia}/evidencias", async (Guid idBestia, ClaimsPrincipal user, IEvidenciaService service) =>
            {
                var dtos = await service.GetByBestiaAsync(idBestia);
                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!esInvestigador)
                {
                    dtos = dtos.Where(e => e.Estado != null && e.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return Results.Ok(dtos);
            })
            .WithName("GetEvidenciasByBestia")
            .AllowAnonymous()
            .Produces<List<EvidenciaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/bestias/{idBestia}/evidencias/{nroEvidencia:int}", async (Guid idBestia, int nroEvidencia, ClaimsPrincipal user, IEvidenciaService service) =>
            {
                var dto = await service.GetAsync(idBestia, nroEvidencia);
                if (dto == null)
                    return Results.NotFound();

                bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!esInvestigador && !dto.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase))
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetEvidencia")
            .AllowAnonymous()
            .Produces<EvidenciaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/bestias/{idBestia}/evidencias", async (Guid idBestia, EvidenciaDTO dto, ClaimsPrincipal user, IEvidenciaService service) =>
            {
                try
                {
                    dto.IdBestia = idBestia;
                    bool esInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                    if (!esInvestigador)
                    {
                        dto.Estado = "pendiente";
                    }

                    var result = await service.AddAsync(dto);
                    return Results.Created($"/bestias/{idBestia}/evidencias/{result.NroEvidencia}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddEvidencia")
            .RequireAuthorization()
            .Produces<EvidenciaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPut("/bestias/{idBestia}/evidencias", async (Guid idBestia, EvidenciaDTO dto, IEvidenciaService service) =>
            {
                try
                {
                    dto.IdBestia = idBestia;
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
            .WithName("UpdateEvidencia")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/bestias/{idBestia}/evidencias/{nroEvidencia:int}", async (Guid idBestia, int nroEvidencia, IEvidenciaService service) =>
            {
                var deleted = await service.DeleteAsync(idBestia, nroEvidencia);
                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteEvidencia")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPut("/bestias/{idBestia}/evidencias/{nroEvidencia:int}/aprobar", async (Guid idBestia, int nroEvidencia, IEvidenciaService service) =>
            {
                var ok = await service.AprobarAsync(idBestia, nroEvidencia);
                if (!ok)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("AprobarEvidencia")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}

