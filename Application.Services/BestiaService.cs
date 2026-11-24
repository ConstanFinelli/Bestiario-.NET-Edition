using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class BestiaService : IBestiaService
    {
        private readonly IBestiaRepository bestiaRepository;
        private readonly IRegistroRepository? registroRepository;
        private readonly ICloudinaryService? cloudinaryService;

        public BestiaService(
            IBestiaRepository bestiaRepository,
            IRegistroRepository? registroRepository = null,
            ICloudinaryService? cloudinaryService = null)
        {
            this.bestiaRepository = bestiaRepository;
            this.registroRepository = registroRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<BestiaDTO> AddAsync(BestiaDTO dto)
        {
            var bestia = new Bestia(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.Nombre,
                dto.Peligrosidad,
                string.IsNullOrWhiteSpace(dto.Estado) ? "pendiente" : dto.Estado,
                dto.IdHabitat
            );

            await bestiaRepository.AddAsync(bestia, dto.CategoriasIds);

            dto.Id = bestia.Id;
            dto.Nombre = bestia.Nombre;
            dto.Peligrosidad = bestia.Peligrosidad;
            dto.Estado = bestia.Estado;
            dto.IdHabitat = bestia.IdHabitat;

            return dto;
        }

        public async Task<bool> UpdateAsync(BestiaDTO dto)
        {
            var bestia = new Bestia(
                dto.Id,
                dto.Nombre,
                dto.Peligrosidad,
                dto.Estado,
                dto.IdHabitat
            );

            return await bestiaRepository.UpdateAsync(bestia, dto.CategoriasIds);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (registroRepository != null && cloudinaryService != null)
            {
                var registros = await registroRepository.GetByBestiaAsync(id);
                var publicIds = registros
                    .Select(r => r.MainPicture)
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Distinct(StringComparer.OrdinalIgnoreCase);

                foreach (var publicId in publicIds)
                {
                    await cloudinaryService.DeleteImageAsync(publicId);
                }
            }

            return await bestiaRepository.DeleteAsync(id);
        }

        public async Task<BestiaDTO?> GetAsync(Guid id)
        {
            var bestia = await bestiaRepository.GetAsync(id);
            return bestia == null ? null : MapToDTO(bestia);
        }

        public async Task<IEnumerable<BestiaDTO>> GetAllAsync()
        {
            var bestias = await bestiaRepository.GetAllAsync();
            return bestias.Select(MapToDTO).ToList();
        }

        private BestiaDTO MapToDTO(Bestia b)
        {
            var ultimoAprobado = b.Registros?
                .Where(r => string.Equals(r.Estado, "aprobado", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(r => r.FechaAprobacion ?? DateTime.MinValue)
                .ThenByDescending(r => r.NroRegistro)
                .FirstOrDefault();

            return new BestiaDTO
            {
                Id = b.Id,
                Nombre = b.Nombre,
                Peligrosidad = b.Peligrosidad,
                Estado = b.Estado,
                IdHabitat = b.IdHabitat,
                HabitatNombre = b.Habitat?.Nombre,
                ImagenUrl = cloudinaryService?.GetImagenListadoBestia(ultimoAprobado?.MainPicture),
                CategoriasIds = b.Categorias.Select(c => c.Id).ToList(),
                CategoriasNombres = b.Categorias.Select(c => c.Nombre).ToList()
            };
        }
    }
}
