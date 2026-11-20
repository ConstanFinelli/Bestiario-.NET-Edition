using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class EvidenciaService : IEvidenciaService
    {
        private readonly IEvidenciaRepository evidenciaRepository;

        public EvidenciaService(IEvidenciaRepository evidenciaRepository)
        {
            this.evidenciaRepository = evidenciaRepository;
        }

        public async Task<IEnumerable<EvidenciaDTO>> GetByBestiaAsync(Guid idBestia)
        {
            var items = await evidenciaRepository.GetByBestiaAsync(idBestia);
            return items.Select(MapToDTO).ToList();
        }

        public async Task<EvidenciaDTO?> GetAsync(Guid idBestia, int nroEvidencia)
        {
            var item = await evidenciaRepository.GetAsync(idBestia, nroEvidencia);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<EvidenciaDTO> AddAsync(EvidenciaDTO dto)
        {
            var evidencia = new Evidencia(
                dto.IdBestia,
                dto.NroEvidencia,
                dto.IdTipoEvidencia,
                dto.FechaObtencion == default ? DateTime.Now : dto.FechaObtencion,
                dto.FileId,
                string.IsNullOrWhiteSpace(dto.Estado) ? "pendiente" : dto.Estado
            );

            await evidenciaRepository.AddAsync(evidencia);
            var created = await evidenciaRepository.GetAsync(evidencia.IdBestia, evidencia.NroEvidencia);
            return created != null ? MapToDTO(created) : MapToDTO(evidencia);
        }

        public async Task<bool> UpdateAsync(EvidenciaDTO dto)
        {
            var evidencia = new Evidencia(
                dto.IdBestia,
                dto.NroEvidencia,
                dto.IdTipoEvidencia,
                dto.FechaObtencion,
                dto.FileId,
                dto.Estado
            );

            return await evidenciaRepository.UpdateAsync(evidencia);
        }

        public async Task<bool> DeleteAsync(Guid idBestia, int nroEvidencia)
        {
            return await evidenciaRepository.DeleteAsync(idBestia, nroEvidencia);
        }

        public async Task<bool> AprobarAsync(Guid idBestia, int nroEvidencia)
        {
            return await evidenciaRepository.AprobarAsync(idBestia, nroEvidencia);
        }

        private static EvidenciaDTO MapToDTO(Evidencia e)
        {
            return new EvidenciaDTO
            {
                IdBestia = e.IdBestia,
                NroEvidencia = e.NroEvidencia,
                Estado = e.Estado,
                FechaObtencion = e.FechaObtencion,
                FileId = e.FileId,
                IdTipoEvidencia = e.IdTipoEvidencia,
                TipoEvidenciaDescripcion = e.TipoEvidencia?.Descripcion,
                TipoEvidenciaResourceType = e.TipoEvidencia?.ResourceType
            };
        }
    }
}
