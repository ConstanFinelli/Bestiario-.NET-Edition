using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICloudinaryService
    {
        bool IsConfigured { get; }
        string? DefaultPictureId { get; }

        Task<string?> UploadAsync(Stream fileStream, string fileName, string contentType);
        Task DeleteImageAsync(string? publicId);

        string? GetImagenRegistro(string? publicId);
        string? GetImagenListadoBestia(string? publicId);
        string? GetDefaultImage();
        string? GetImagenEvidencia(string? publicId);
        string? GetVideoEvidencia(string? publicId);
        string? GetArchivoEvidencia(string? publicId);
    }
}
