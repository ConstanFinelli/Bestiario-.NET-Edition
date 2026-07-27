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
            Noticia noticia = new Noticia(dto.Titulo, dto.Contenido, fechaPublicacion/*, dto.PublicadorId */);

            await noticiaRepository.AddAsync(noticia);

            dto.Id = noticia.Id;

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await noticiaRepository.DeleteAsync(id);
        }

        public async Task<NoticiaDTO?> GetAsync(Guid id)
        {
            Noticia? noticia = await noticiaRepository.GetAsync(id);

            if (noticia == null)
                return null;

            return new NoticiaDTO
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Contenido = noticia.Contenido,
                /*PublicadorId = noticia.PublicadorId,*/
                FechaPublicacion = noticia.FechaPublicacion
            };
        }

        public async Task<IEnumerable<NoticiaDTO>> GetAllAsync()
        {
            var noticias = await noticiaRepository.GetAllAsync();

            return noticias.Select(noticia => new NoticiaDTO
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Contenido = noticia.Contenido,
                /*PublicadorId = noticia.PublicadorId,*/
                FechaPublicacion = noticia.FechaPublicacion
            }).ToList();
        }

        public async Task<bool> UpdateAsync(NoticiaDTO dto)
        {

            Noticia noticia = new Noticia(dto.Id, dto.Titulo, dto.Contenido, dto.FechaPublicacion/*, dto.PublicadorId */);
            return await noticiaRepository.UpdateAsync(noticia);
        }
    }
}
