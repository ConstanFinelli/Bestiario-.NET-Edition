using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly string _connectionString;

        public ReporteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public async Task<ReporteEstadisticasBestiasDTO> GetEstadisticasBestiasAsync()
        {
            var resultado = new ReporteEstadisticasBestiasDTO();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // 1. Totales generales
            const string sqlTotales = @"
SELECT
    (SELECT COUNT(*) FROM Bestias) AS TotalBestias,
    (SELECT COUNT(*) FROM Bestias WHERE LOWER(Estado) = 'aprobado') AS TotalAprobadas,
    (SELECT COUNT(*) FROM Bestias WHERE LOWER(Estado) = 'pendiente') AS TotalPendientes,
    (SELECT COUNT(*) FROM Registros) AS TotalRegistros,
    (SELECT COUNT(*) FROM Evidencias) AS TotalEvidencias;";

            await using (var cmd = new SqlCommand(sqlTotales, connection) { CommandType = CommandType.Text })
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    resultado.TotalBestias = reader.GetInt32(0);
                    resultado.TotalAprobadas = reader.GetInt32(1);
                    resultado.TotalPendientes = reader.GetInt32(2);
                    resultado.TotalRegistros = reader.GetInt32(3);
                    resultado.TotalEvidencias = reader.GetInt32(4);
                }
            }

            // 2. Desglose por Categoría (JOIN N:M BestiaCategorias)
            const string sqlCategorias = @"
SELECT
    c.Nombre AS Categoria,
    COUNT(b.Id) AS CantidadBestias,
    ISNULL(SUM(CASE WHEN LOWER(b.Estado) = 'aprobado' THEN 1 ELSE 0 END), 0) AS Aprobadas,
    ISNULL(SUM(CASE WHEN LOWER(b.Estado) = 'pendiente' THEN 1 ELSE 0 END), 0) AS Pendientes
FROM Categorias c
LEFT JOIN BestiaCategorias bc ON bc.CategoriasId = c.Id
LEFT JOIN Bestias b ON b.Id = bc.BestiasId
GROUP BY c.Nombre
ORDER BY CantidadBestias DESC, c.Nombre ASC;";

            await using (var cmd = new SqlCommand(sqlCategorias, connection) { CommandType = CommandType.Text })
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    resultado.PorCategoria.Add(new ReporteBestiasPorCategoriaDTO
                    {
                        Categoria = reader.GetString(0),
                        CantidadBestias = reader.GetInt32(1),
                        Aprobadas = reader.GetInt32(2),
                        Pendientes = reader.GetInt32(3)
                    });
                }
            }

            // 3. Desglose por Peligrosidad
            const string sqlPeligrosidad = @"
SELECT
    b.Peligrosidad,
    COUNT(*) AS Cantidad,
    ISNULL(SUM(CASE WHEN LOWER(b.Estado) = 'aprobado' THEN 1 ELSE 0 END), 0) AS Aprobadas,
    ISNULL(SUM(CASE WHEN LOWER(b.Estado) = 'pendiente' THEN 1 ELSE 0 END), 0) AS Pendientes
FROM Bestias b
GROUP BY b.Peligrosidad
ORDER BY Cantidad DESC, b.Peligrosidad ASC;";

            await using (var cmd = new SqlCommand(sqlPeligrosidad, connection) { CommandType = CommandType.Text })
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    resultado.PorPeligrosidad.Add(new ReporteBestiasPorPeligrosidadDTO
                    {
                        Peligrosidad = reader.GetString(0),
                        Cantidad = reader.GetInt32(1),
                        Aprobadas = reader.GetInt32(2),
                        Pendientes = reader.GetInt32(3)
                    });
                }
            }

            return resultado;
        }

        public async Task<IEnumerable<ReporteRegistrosPorBestiaDTO>> GetRegistrosPorBestiaAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string? estado = null,
            Guid? idBestia = null)
        {
            var lista = new List<ReporteRegistrosPorBestiaDTO>();

            DateTime? hastaFin = hasta.HasValue
                ? hasta.Value.Date.AddDays(1).AddTicks(-1)
                : null;

            const string sql = @"
SELECT
    r.IdBestia,
    b.Nombre AS BestiaNombre,
    b.Peligrosidad,
    ISNULL(h.Nombre, 'Sin hábitat') AS HabitatNombre,
    r.NroRegistro,
    r.FechaAprobacion,
    r.Estado,
    ISNULL(up.Correo, 'N/A') AS PublicadorCorreo,
    ISNULL(NULLIF(LTRIM(RTRIM(CONCAT(ISNULL(ua.Nombre, ''), ' ', ISNULL(ua.Apellido, '')))), ''), '-') AS AprobadorNombre,
    COUNT(cr.NroContenido) AS CantidadContenidos
FROM Registros r
INNER JOIN Bestias b ON b.Id = r.IdBestia
LEFT JOIN Habitats h ON h.Id = b.IdHabitat
LEFT JOIN Usuarios up ON up.Id = r.IdUsuarioPublicador
LEFT JOIN Usuarios ua ON ua.Id = r.IdInvestigadorAprobador
LEFT JOIN ContenidoRegistros cr ON cr.IdBestia = r.IdBestia AND cr.NroRegistro = r.NroRegistro
WHERE (@Estado IS NULL OR LOWER(r.Estado) = LOWER(@Estado))
  AND (@IdBestia IS NULL OR r.IdBestia = @IdBestia)
  AND (@Desde IS NULL OR (r.FechaAprobacion IS NOT NULL AND r.FechaAprobacion >= @Desde))
  AND (@Hasta IS NULL OR (r.FechaAprobacion IS NOT NULL AND r.FechaAprobacion <= @Hasta))
GROUP BY
    r.IdBestia, b.Nombre, b.Peligrosidad, h.Nombre,
    r.NroRegistro, r.FechaAprobacion, r.Estado,
    up.Correo, ua.Nombre, ua.Apellido
ORDER BY b.Nombre ASC, r.NroRegistro ASC;";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var cmd = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
            cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.NVarChar, 20)
            {
                Value = string.IsNullOrWhiteSpace(estado) ? DBNull.Value : estado.Trim()
            });
            cmd.Parameters.Add(new SqlParameter("@IdBestia", SqlDbType.UniqueIdentifier)
            {
                Value = idBestia.HasValue && idBestia.Value != Guid.Empty ? idBestia.Value : DBNull.Value
            });
            cmd.Parameters.Add(new SqlParameter("@Desde", SqlDbType.DateTime2)
            {
                Value = desde.HasValue ? desde.Value.Date : DBNull.Value
            });
            cmd.Parameters.Add(new SqlParameter("@Hasta", SqlDbType.DateTime2)
            {
                Value = hastaFin.HasValue ? hastaFin.Value : DBNull.Value
            });

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new ReporteRegistrosPorBestiaDTO
                {
                    IdBestia = reader.GetGuid(0),
                    BestiaNombre = reader.GetString(1),
                    Peligrosidad = reader.GetString(2),
                    HabitatNombre = reader.GetString(3),
                    NroRegistro = reader.GetInt32(4),
                    FechaAprobacion = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Estado = reader.GetString(6),
                    PublicadorCorreo = reader.GetString(7),
                    AprobadorNombre = reader.GetString(8),
                    CantidadContenidos = reader.GetInt32(9)
                });
            }

            return lista;
        }
    }
}
