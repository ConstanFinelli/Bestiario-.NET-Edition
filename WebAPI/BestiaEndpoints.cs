using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class BestiaEndpoints
    {
        public static void MapBestiaEndpoints(this WebApplication app)
        {
            app.MapGet("/bestias/{id}", async (Guid id, ClaimsPrincipal user, IBestiaService bestiaService) =>
            {
                var dto = await bestiaService.GetAsync(id);
                if (dto == null)
                    return Results.NotFound();

                bool isInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!isInvestigador && !dto.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase))
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetBestia")
            .AllowAnonymous()
            .Produces<BestiaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/bestias", async (ClaimsPrincipal user, IBestiaService bestiaService) =>
            {
                var dtos = await bestiaService.GetAllAsync();
                bool isInvestigador = user.Identity?.IsAuthenticated == true && user.IsInRole("Investigador");
                if (!isInvestigador)
                {
                    dtos = dtos.Where(b => b.Estado != null && b.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }
                return Results.Ok(dtos);
            })
            .WithName("GetAllBestias")
            .AllowAnonymous()
            .Produces<List<BestiaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/bestias", async (BestiaDTO dto, IBestiaService bestiaService) =>
            {
                try
                {
                    var result = await bestiaService.AddAsync(dto);
                    return Results.Created($"/bestias/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddBestia")
            .RequireAuthorization()
            .Produces<BestiaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPut("/bestias", async (BestiaDTO dto, IBestiaService bestiaService) =>
            {
                try
                {
                    var found = await bestiaService.UpdateAsync(dto);
                    if (!found)
                        return Results.NotFound();

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateBestia")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/bestias/{id}", async (Guid id, IBestiaService bestiaService) =>
            {
                var deleted = await bestiaService.DeleteAsync(id);
                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteBestia")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
