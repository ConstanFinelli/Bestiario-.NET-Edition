using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class HabitatEndpoints
    {
        public static void MapHabitatEndpoints(this WebApplication app)
        {
            app.MapGet("/habitats/{id}", async (Guid id, IHabitatService habitatService) =>
            {
                var dto = await habitatService.GetAsync(id);
                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetHabitat")
            .AllowAnonymous()
            .Produces<HabitatDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/habitats", async (IHabitatService habitatService) =>
            {
                var dtos = await habitatService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllHabitats")
            .AllowAnonymous()
            .Produces<List<HabitatDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/habitats", async (HabitatDTO dto, IHabitatService habitatService) =>
            {
                try
                {
                    var result = await habitatService.AddAsync(dto);
                    return Results.Created($"/habitats/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddHabitat")
            .RequireAuthorization()
            .Produces<HabitatDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();

            app.MapPut("/habitats", async (HabitatDTO dto, IHabitatService habitatService) =>
            {
                try
                {
                    var found = await habitatService.UpdateAsync(dto);
                    if (!found)
                        return Results.NotFound();

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateHabitat")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/habitats/{id}", async (Guid id, IHabitatService habitatService) =>
            {
                var deleted = await habitatService.DeleteAsync(id);
                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteHabitat")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}

