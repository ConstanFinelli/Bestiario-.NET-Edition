using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class HabitatService : IHabitatService
    {
        private readonly IHabitatRepository habitatRepository;

        public HabitatService(IHabitatRepository habitatRepository)
        {
            this.habitatRepository = habitatRepository;
        }

        public async Task<IEnumerable<HabitatDTO>> GetAllAsync()
        {
            var habitats = await habitatRepository.GetAllAsync();
            return habitats.Select(MapToDTO).ToList();
        }

        public async Task<HabitatDTO?> GetAsync(Guid id)
        {
            var habitat = await habitatRepository.GetAsync(id);
            return habitat != null ? MapToDTO(habitat) : null;
        }

        public async Task<HabitatDTO> AddAsync(HabitatDTO dto)
        {
            var habitat = new Habitat(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.Nombre,
                dto.Localizacion,
                dto.Latitud,
                dto.Longitud
            );

            if (dto.Caracteristicas != null)
            {
                foreach (var c in dto.Caracteristicas)
                {
                    habitat.AgregarCaracteristica(c.Descripcion);
                }
            }

            await habitatRepository.AddAsync(habitat);
            return MapToDTO(habitat);
        }

        public async Task<bool> UpdateAsync(HabitatDTO dto)
        {
            var habitat = new Habitat(
                dto.Id,
                dto.Nombre,
                dto.Localizacion,
                dto.Latitud,
                dto.Longitud
            );

            if (dto.Caracteristicas != null)
            {
                foreach (var c in dto.Caracteristicas)
                {
                    habitat.AgregarCaracteristica(c.Descripcion);
                }
            }

            return await habitatRepository.UpdateAsync(habitat);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await habitatRepository.DeleteAsync(id);
        }

        private static HabitatDTO MapToDTO(Habitat h)
        {
            return new HabitatDTO
            {
                Id = h.Id,
                Nombre = h.Nombre,
                Localizacion = h.Localizacion,
                Latitud = h.Latitud,
                Longitud = h.Longitud,
                Caracteristicas = h.Caracteristicas.Select(c => new CaracteristicaHabitatDTO
                {
                    IdHabitat = c.IdHabitat,
                    NroCaracteristica = c.NroCaracteristica,
                    Descripcion = c.Descripcion
                }).OrderBy(c => c.NroCaracteristica).ToList()
            };
        }
    }
}

