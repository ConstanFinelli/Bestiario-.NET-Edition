using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class SolicitudInvestigadorApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<SolicitudInvestigadorDTO>> GetAllAsync(string? estado = null)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                string url = string.IsNullOrWhiteSpace(estado)
                    ? "solicitudes-investigador"
                    : $"solicitudes-investigador?estado={Uri.EscapeDataString(estado)}";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<SolicitudInvestigadorDTO>>();
                    return result ?? new List<SolicitudInvestigadorDTO>();
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener solicitudes. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener solicitudes: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener solicitudes: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<SolicitudInvestigadorDTO>> GetMisSolicitudesAsync()
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync("solicitudes-investigador/mias");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<SolicitudInvestigadorDTO>>();
                    return result ?? new List<SolicitudInvestigadorDTO>();
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener tus solicitudes. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener tus solicitudes: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener tus solicitudes: {ex.Message}", ex);
            }
        }

        public static async Task<SolicitudInvestigadorDTO> CrearAsync(CrearSolicitudInvestigadorDTO dto)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PostAsJsonAsync("solicitudes-investigador", dto);

                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SolicitudInvestigadorDTO>();
                    return created ?? new SolicitudInvestigadorDTO();
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al enviar solicitud. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al enviar solicitud: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al enviar solicitud: {ex.Message}", ex);
            }
        }

        public static async Task AprobarAsync(Guid id)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PutAsync($"solicitudes-investigador/{id}/aprobar", null);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al aprobar solicitud. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al aprobar solicitud: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al aprobar solicitud: {ex.Message}", ex);
            }
        }

        public static async Task RechazarAsync(Guid id)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PutAsync($"solicitudes-investigador/{id}/rechazar", null);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al rechazar solicitud. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al rechazar solicitud: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al rechazar solicitud: {ex.Message}", ex);
            }
        }
    }
}
