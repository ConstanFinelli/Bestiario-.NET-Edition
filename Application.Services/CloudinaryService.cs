using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private const int MaxWidth = 1920;
        private const int MaxHeight = 1080;
        private const long MaxBytes = 5L * 1024L * 1024L;

        private readonly Cloudinary? _cloudinary;
        private readonly string? _defaultPictureId;

        public bool IsConfigured => _cloudinary != null;
        public string? DefaultPictureId => _defaultPictureId;

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

        public async Task<string?> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            if (_cloudinary == null)
            {
                throw new InvalidOperationException("Cloudinary no está configurado. Configure CLOUD_NAME, API_KEY y API_SECRET.");
            }

            using var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            byte[] originalBytes = ms.ToArray();
            byte[] bytesToUpload = originalBytes;

            if (!string.IsNullOrWhiteSpace(contentType)
                && contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            {
                bytesToUpload = ProcesarImagenLocal(originalBytes);
            }

            using var uploadStream = new MemoryStream(bytesToUpload);
            var uploadParams = new AutoUploadParams
            {
                File = new FileDescription(fileName, uploadStream),
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

        private static byte[] ProcesarImagenLocal(byte[] originalBytes)
        {
            try
            {
                using var image = Image.Load<Rgba32>(originalBytes);
                bool excedeDimensiones = image.Width > MaxWidth || image.Height > MaxHeight;
                bool excedeTamanio = originalBytes.Length > MaxBytes;

                if (!excedeDimensiones && !excedeTamanio)
                {
                    return originalBytes;
                }

                if (excedeDimensiones)
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new SixLabors.ImageSharp.Size(MaxWidth, MaxHeight),
                        Mode = ResizeMode.Max
                    }));
                }

                bool tieneAlfa = TieneTransparencia(image);
                using var outMs = new MemoryStream();
                if (tieneAlfa)
                {
                    image.Save(outMs, new PngEncoder());
                }
                else
                {
                    image.Save(outMs, new JpegEncoder { Quality = 85 });
                }

                return outMs.ToArray();
            }
            catch
            {
                return originalBytes;
            }
        }

        private static bool TieneTransparencia(Image<Rgba32> image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image[x, y].A < 255)
                        return true;
                }
            }
            return false;
        }

        public async Task DeleteImageAsync(string? publicId)
        {
            if (_cloudinary == null || string.IsNullOrWhiteSpace(publicId))
                return;

            if (!string.IsNullOrWhiteSpace(_defaultPictureId)
                && string.Equals(publicId.Trim(), _defaultPictureId.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var delParams = new DeletionParams(publicId.Trim())
            {
                ResourceType = ResourceType.Image
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
