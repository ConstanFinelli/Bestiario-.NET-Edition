using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class TipoEvidenciaEndpoints
    {
        public static void MapTipoEvidenciaEndpoints(this WebApplication app)
        {
            app.MapGet("/tiposevidencias/{id}", async (Guid id, ITipoEvidenciaService service) =>
            {
                var dto = await service.GetAsync(id);
                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetTipoEvidencia")
            .AllowAnonymous()
            .Produces<TipoEvidenciaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/tiposevidencias", async (ITipoEvidenciaService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllTiposEvidencia")
            .AllowAnonymous()
            .Produces<List<TipoEvidenciaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/tiposevidencias", async (TipoEvidenciaDTO dto, ITipoEvidenciaService service) =>
            {
                try
                {
                    var result = await service.AddAsync(dto);
                    return Results.Created($"/tiposevidencias/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddTipoEvidencia")
            .RequireAuthorization()
            .Produces<TipoEvidenciaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPut("/tiposevidencias", async (TipoEvidenciaDTO dto, ITipoEvidenciaService service) =>
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
            .WithName("UpdateTipoEvidencia")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/tiposevidencias/{id}", async (Guid id, ITipoEvidenciaService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteTipoEvidencia")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}

