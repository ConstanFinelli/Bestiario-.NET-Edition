using System;
using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            app.MapGet("/reportes/bestias-estadisticas", async (IReporteService service) =>
            {
                var dto = await service.GetEstadisticasBestiasAsync();
                return Results.Ok(dto);
            })
            .WithName("GetReporteEstadisticasBestias")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces<ReporteEstadisticasBestiasDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi();

            app.MapGet("/reportes/registros-por-bestia", async (
                DateTime? desde,
                DateTime? hasta,
                string? estado,
                Guid? idBestia,
                IReporteService service) =>
            {
                var dtos = await service.GetRegistrosPorBestiaAsync(desde, hasta, estado, idBestia);
                return Results.Ok(dtos);
            })
            .WithName("GetReporteRegistrosPorBestia")
            .RequireAuthorization(policy => policy.RequireRole("Investigador"))
            .Produces<List<ReporteRegistrosPorBestiaDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi();
        }
    }
}
