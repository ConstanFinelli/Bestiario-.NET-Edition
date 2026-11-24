using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICloudinaryService
    {
        bool IsConfigured { get; }

        Task<string?> UploadAsync(Stream fileStream, string fileName);
        Task DeleteImageAsync(string? publicId, string? resourceType = null);

        string? GetImagenRegistro(string? publicId);
        string? GetImagenListadoBestia(string? publicId);
        string? GetDefaultImage();
        string? GetImagenEvidencia(string? publicId);
        string? GetVideoEvidencia(string? publicId);
        string? GetArchivoEvidencia(string? publicId);
    }
}
