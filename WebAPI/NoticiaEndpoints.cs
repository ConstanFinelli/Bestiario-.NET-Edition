using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class NoticiaEndpoints
    {
        public static void MapNoticiaEndpoints(this WebApplication app)
        {
            app.MapGet("/noticias/{id}", async (Guid id, INoticiaService noticiaService) =>
            {
                NoticiaDTO? dto = await noticiaService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetNoticia")
            .Produces<NoticiaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/noticias", async (INoticiaService noticiaService) =>
            {
                var dtos = await noticiaService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllNoticias")
            .Produces<List<NoticiaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/noticias", async (NoticiaDTO dto, INoticiaService noticiaService) =>
            {
                try
                {
                    NoticiaDTO noticiaDTO = await noticiaService.AddAsync(dto);

                    return Results.Created($"/noticias/{noticiaDTO.Id}", noticiaDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddNoticia")
            .Produces<NoticiaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/noticias", async (NoticiaDTO dto, INoticiaService noticiaService) =>
            {
                try
                {
                    var found = await noticiaService.UpdateAsync(dto);

                    if (!found)
                    {
                        return Results.NotFound();
                    }

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateNoticia")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/noticias/{id}", async (Guid id, INoticiaService noticiaService) =>
            {
                var deleted = await noticiaService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteNoticia")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}