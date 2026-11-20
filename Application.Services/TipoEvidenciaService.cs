using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class TipoEvidenciaService : ITipoEvidenciaService
    {
        private readonly ITipoEvidenciaRepository tipoEvidenciaRepository;

        public TipoEvidenciaService(ITipoEvidenciaRepository tipoEvidenciaRepository)
        {
            this.tipoEvidenciaRepository = tipoEvidenciaRepository;
        }

        public async Task<IEnumerable<TipoEvidenciaDTO>> GetAllAsync()
        {
            var items = await tipoEvidenciaRepository.GetAllAsync();
            return items.Select(t => new TipoEvidenciaDTO
            {
                Id = t.Id,
                Descripcion = t.Descripcion,
                ResourceType = t.ResourceType
            }).ToList();
        }

        public async Task<TipoEvidenciaDTO?> GetAsync(Guid id)
        {
            var item = await tipoEvidenciaRepository.GetAsync(id);
            if (item == null)
                return null;

            return new TipoEvidenciaDTO
            {
                Id = item.Id,
                Descripcion = item.Descripcion,
                ResourceType = item.ResourceType
            };
        }

        public async Task<TipoEvidenciaDTO> AddAsync(TipoEvidenciaDTO dto)
        {
            var tipo = new TipoEvidencia(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.Descripcion,
                dto.ResourceType
            );

            await tipoEvidenciaRepository.AddAsync(tipo);

            dto.Id = tipo.Id;
            dto.Descripcion = tipo.Descripcion;
            dto.ResourceType = tipo.ResourceType;

            return dto;
        }

        public async Task<bool> UpdateAsync(TipoEvidenciaDTO dto)
        {
            var tipo = new TipoEvidencia(dto.Id, dto.Descripcion, dto.ResourceType);
            return await tipoEvidenciaRepository.UpdateAsync(tipo);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await tipoEvidenciaRepository.DeleteAsync(id);
        }
    }
}

