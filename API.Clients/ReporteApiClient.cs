using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class ReporteApiClient : BaseApiClient
    {
        public static async Task<ReporteEstadisticasBestiasDTO> GetEstadisticasBestiasAsync()
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync("reportes/bestias-estadisticas");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ReporteEstadisticasBestiasDTO>();
                    return result ?? new ReporteEstadisticasBestiasDTO();
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener estadísticas de bestias. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener estadísticas de bestias: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener estadísticas de bestias: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<ReporteRegistrosPorBestiaDTO>> GetRegistrosPorBestiaAsync(
            DateTime? desde = null,
            DateTime? hasta = null,
            string? estado = null,
            Guid? idBestia = null)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                var queryParts = new List<string>();

                if (desde.HasValue)
                    queryParts.Add($"desde={Uri.EscapeDataString(desde.Value.ToString("yyyy-MM-dd"))}");
                if (hasta.HasValue)
                    queryParts.Add($"hasta={Uri.EscapeDataString(hasta.Value.ToString("yyyy-MM-dd"))}");
                if (!string.IsNullOrWhiteSpace(estado))
                    queryParts.Add($"estado={Uri.EscapeDataString(estado.Trim())}");
                if (idBestia.HasValue && idBestia.Value != Guid.Empty)
                    queryParts.Add($"idBestia={idBestia.Value}");

                string url = queryParts.Count > 0
                    ? $"reportes/registros-por-bestia?{string.Join("&", queryParts)}"
                    : "reportes/registros-por-bestia";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<ReporteRegistrosPorBestiaDTO>>();
                    return result ?? new List<ReporteRegistrosPorBestiaDTO>();
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener reporte de registros. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener reporte de registros: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener reporte de registros: {ex.Message}", ex);
            }
        }
    }
}
