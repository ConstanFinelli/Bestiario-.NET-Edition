using Application.Services;

namespace WebAPI
{
    public static class CloudinaryEndpoints
    {
        public static void MapCloudinaryEndpoints(this WebApplication app)
        {
            app.MapPost("/cloudinary/upload", async (IFormFile file, ICloudinaryService cloudinaryService) =>
            {
                if (file == null || file.Length == 0)
                {
                    return Results.BadRequest(new { error = "Debe adjuntar un archivo válido." });
                }

                try
                {
                    await using var stream = file.OpenReadStream();
                    string? publicId = await cloudinaryService.UploadAsync(stream, file.FileName);
                    string? url = cloudinaryService.GetImagenRegistro(publicId);
                    return Results.Ok(new { publicId, url });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al subir archivo a Cloudinary: {ex.Message}");
                }
            })
            .WithName("UploadCloudinaryFile")
            .RequireAuthorization()
            .DisableAntiforgery()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            app.MapDelete("/cloudinary/{*publicId}", async (string publicId, ICloudinaryService cloudinaryService) =>
            {
                if (string.IsNullOrWhiteSpace(publicId))
                    return Results.BadRequest();

                await cloudinaryService.DeleteImageAsync(Uri.UnescapeDataString(publicId));
                return Results.NoContent();
            })
            .WithName("DeleteCloudinaryFile")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }
}
