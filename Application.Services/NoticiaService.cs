using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly INoticiaRepository noticiaRepository;

        public NoticiaService(INoticiaRepository noticiaRepository)
        {
            this.noticiaRepository = noticiaRepository;
        }

        public async Task<NoticiaDTO> AddAsync(NoticiaDTO dto)
        {
            var fechaPublicacion = DateTime.Now;
            Noticia noticia = new Noticia(dto.Titulo, dto.Contenido, fechaPublicacion, dto.PublicadorId);

            await noticiaRepository.AddAsync(noticia);

            var creada = await noticiaRepository.GetAsync(noticia.Id);
            if (creada != null)
            {
                return MapToDTO(creada);
            }

            dto.Id = noticia.Id;
            dto.FechaPublicacion = fechaPublicacion;
            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await noticiaRepository.DeleteAsync(id);
        }

        public async Task<NoticiaDTO?> GetAsync(Guid id)
        {
            Noticia? noticia = await noticiaRepository.GetAsync(id);
            return noticia != null ? MapToDTO(noticia) : null;
        }

        public async Task<IEnumerable<NoticiaDTO>> GetAllAsync()
        {
            var noticias = await noticiaRepository.GetAllAsync();
            return noticias.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(NoticiaDTO dto)
        {
            Noticia noticia = new Noticia(dto.Id, dto.Titulo, dto.Contenido, dto.FechaPublicacion, dto.PublicadorId);
            return await noticiaRepository.UpdateAsync(noticia);
        }

        private static NoticiaDTO MapToDTO(Noticia noticia)
        {
            string? nombrePublicador = null;
            if (noticia.Publicador != null)
            {
                string nombreCompleto = $"{noticia.Publicador.Nombre} {noticia.Publicador.Apellido}".Trim();
                nombrePublicador = string.IsNullOrWhiteSpace(nombreCompleto)
                    ? noticia.Publicador.Correo
                    : nombreCompleto;
            }

            return new NoticiaDTO
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo ?? string.Empty,
                Contenido = noticia.Contenido ?? string.Empty,
                FechaPublicacion = noticia.FechaPublicacion,
                PublicadorId = noticia.PublicadorId,
                NombrePublicador = nombrePublicador
            };
        }
    }
}
