using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class BestiaEndpoints
    {
        public static void MapBestiaEndpoints(this WebApplication app)
        {
            app.MapGet("/bestias/{id}", async (Guid id, IBestiaService bestiaService) =>
            {
                var dto = await bestiaService.GetAsync(id);
                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetBestia")
            .Produces<BestiaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/bestias", async (IBestiaService bestiaService) =>
            {
                var dtos = await bestiaService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllBestias")
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
            .Produces<BestiaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
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
            .Produces(StatusCodes.Status204NoContent)
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
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
