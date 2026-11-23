using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class CloudinaryApiClient : BaseApiClient
    {
        private class UploadResponse
        {
            public string? PublicId { get; set; }
            public string? Url { get; set; }
        }

        public static async Task<string?> UploadImageAsync(Stream fileStream, string fileName, string contentType)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                using var form = new MultipartFormDataContent();
                using var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(
                    string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType);

                form.Add(streamContent, "file", fileName);

                HttpResponseMessage response = await client.PostAsync("cloudinary/upload", form);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                    return result?.PublicId;
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al subir imagen. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al subir imagen: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al subir imagen: {ex.Message}", ex);
            }
        }

        public static async Task DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId)) return;

            try
            {
                using var client = await CreateHttpClientAsync();
                await client.DeleteAsync($"cloudinary/{Uri.EscapeDataString(publicId)}");
            }
            catch
            {
                // Rollback silencioso
            }
        }
    }
}
