using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private const int MaxWidth = 1920;
        private const int MaxHeight = 1080;

        private readonly Cloudinary? _cloudinary;
        private readonly string? _defaultPictureId;

        public bool IsConfigured => _cloudinary != null;

        public CloudinaryService(IConfiguration configuration)
        {
            string? cloudName = Environment.GetEnvironmentVariable("CLOUD_NAME")
                ?? configuration["Cloudinary:CloudName"];
            string? apiKey = Environment.GetEnvironmentVariable("API_KEY")
                ?? configuration["Cloudinary:ApiKey"];
            string? apiSecret = Environment.GetEnvironmentVariable("API_SECRET")
                ?? configuration["Cloudinary:ApiSecret"];
            _defaultPictureId = Environment.GetEnvironmentVariable("DEFAULT_PICTURE_ID")
                ?? configuration["Cloudinary:DefaultPictureId"];

            if (!string.IsNullOrWhiteSpace(cloudName)
                && !string.IsNullOrWhiteSpace(apiKey)
                && !string.IsNullOrWhiteSpace(apiSecret))
            {
                var account = new Account(cloudName.Trim(), apiKey.Trim(), apiSecret.Trim());
                _cloudinary = new Cloudinary(account) { Api = { Secure = true } };
            }
        }

        public async Task<string?> UploadAsync(Stream fileStream, string fileName)
        {
            if (_cloudinary == null)
            {
                throw new InvalidOperationException("Cloudinary no está configurado. Configure CLOUD_NAME, API_KEY y API_SECRET.");
            }

            // Cloudinary aplica la transformación entrante al subir: limita a 1920x1080
            // manteniendo proporción (crop "limit") y optimiza la calidad automáticamente.
            var uploadParams = new AutoUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Transformation = new Transformation()
                    .Width(MaxWidth)
                    .Height(MaxHeight)
                    .Crop("limit")
                    .Quality("auto")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error != null)
            {
                throw new Exception($"Error de Cloudinary al subir archivo: {result.Error.Message}");
            }

            return result.PublicId;
        }

        public async Task DeleteImageAsync(string? publicId, string? resourceType = null)
        {
            if (_cloudinary == null || string.IsNullOrWhiteSpace(publicId))
                return;

            if (!string.IsNullOrWhiteSpace(_defaultPictureId)
                && string.Equals(publicId.Trim(), _defaultPictureId.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var resType = (resourceType ?? string.Empty).Trim().ToLowerInvariant() switch
            {
                "video" or "audio" => ResourceType.Video,
                "raw" or "document" => ResourceType.Raw,
                _ => ResourceType.Image
            };

            var delParams = new DeletionParams(publicId.Trim())
            {
                ResourceType = resType
            };

            await _cloudinary.DestroyAsync(delParams);
        }

        public string? GetImagenRegistro(string? publicId)
        {
            if (_cloudinary == null) return null;
            string? id = ResolverPublicId(publicId);
            if (string.IsNullOrWhiteSpace(id)) return null;

            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation().Width(MaxWidth).Height(MaxHeight).Crop("limit").Quality("auto").FetchFormat("auto"))
                .BuildUrl(id);
        }

        public string? GetImagenListadoBestia(string? publicId)
        {
            if (_cloudinary == null) return null;
            string? id = ResolverPublicId(publicId);
            if (string.IsNullOrWhiteSpace(id)) return null;

            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation().Width(400).Height(400).Crop("fill").Quality("auto").FetchFormat("auto"))
                .BuildUrl(id);
        }

        public string? GetDefaultImage()
        {
            if (_cloudinary == null || string.IsNullOrWhiteSpace(_defaultPictureId)) return null;

            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation().Width(300).Height(300).Crop("fill").Quality("auto").FetchFormat("auto"))
                .BuildUrl(_defaultPictureId.Trim());
        }

        public string? GetImagenEvidencia(string? publicId)
        {
            if (_cloudinary == null || string.IsNullOrWhiteSpace(publicId)) return null;

            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation().Width(MaxWidth).Height(MaxHeight).Crop("limit").Quality("auto").FetchFormat("auto"))
                .BuildUrl(publicId.Trim());
        }

        public string? GetVideoEvidencia(string? publicId)
        {
            if (_cloudinary == null || string.IsNullOrWhiteSpace(publicId)) return null;

            return _cloudinary.Api.UrlVideoUp
                .Transform(new Transformation().Width(MaxWidth).Height(MaxHeight).Crop("limit").Quality("auto").FetchFormat("auto"))
                .BuildUrl(publicId.Trim());
        }

        public string? GetArchivoEvidencia(string? publicId)
        {
            if (_cloudinary == null || string.IsNullOrWhiteSpace(publicId)) return null;

            return _cloudinary.Api.UrlImgUp
                .ResourceType("raw")
                .Transform(new Transformation().Flags("attachment"))
                .BuildUrl(publicId.Trim());
        }

        private string? ResolverPublicId(string? publicId)
        {
            if (!string.IsNullOrWhiteSpace(publicId))
                return publicId.Trim();

            return string.IsNullOrWhiteSpace(_defaultPictureId) ? null : _defaultPictureId.Trim();
        }
    }
}
