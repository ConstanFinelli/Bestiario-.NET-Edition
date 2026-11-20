using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            this.comentarioRepository = comentarioRepository;
        }

        public async Task<IEnumerable<ComentarioDTO>> GetAllAsync()
        {
            var items = await comentarioRepository.GetAllAsync();
            return items.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<ComentarioDTO>> GetByBestiaAsync(Guid idBestia)
        {
            var items = await comentarioRepository.GetByBestiaAsync(idBestia);
            return items.Select(MapToDTO).ToList();
        }

        public async Task<ComentarioDTO?> GetAsync(Guid id)
        {
            var item = await comentarioRepository.GetAsync(id);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<ComentarioDTO> AddAsync(ComentarioDTO dto)
        {
            var comentario = new Comentario(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.IdBestia,
                dto.IdUsuario,
                dto.Titulo,
                dto.Contenido,
                string.IsNullOrWhiteSpace(dto.Estado) ? "pendiente" : dto.Estado,
                dto.Estado?.Trim().ToLowerInvariant() == "aprobado" ? (dto.FechaAprobacion ?? DateTime.Now) : null
            );

            await comentarioRepository.AddAsync(comentario);
            var created = await comentarioRepository.GetAsync(comentario.Id);
            return created != null ? MapToDTO(created) : MapToDTO(comentario);
        }

        public async Task<bool> UpdateAsync(ComentarioDTO dto)
        {
            var comentario = new Comentario(
                dto.Id,
                dto.IdBestia,
                dto.IdUsuario,
                dto.Titulo,
                dto.Contenido,
                dto.Estado,
                dto.Estado?.Trim().ToLowerInvariant() == "aprobado" ? (dto.FechaAprobacion ?? DateTime.Now) : null
            );

            return await comentarioRepository.UpdateAsync(comentario);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await comentarioRepository.DeleteAsync(id);
        }

        public async Task<bool> AprobarAsync(Guid id)
        {
            return await comentarioRepository.AprobarAsync(id);
        }

        private static ComentarioDTO MapToDTO(Comentario c)
        {
            return new ComentarioDTO
            {
                Id = c.Id,
                IdBestia = c.IdBestia,
                BestiaNombre = c.Bestia?.Nombre,
                IdUsuario = c.IdUsuario,
                CorreoUsuario = c.Usuario?.Correo,
                Titulo = c.Titulo,
                Contenido = c.Contenido,
                Estado = c.Estado,
                FechaAprobacion = c.FechaAprobacion
            };
        }
    }
}

