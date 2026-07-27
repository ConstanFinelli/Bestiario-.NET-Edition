using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data { 

   public class  NoticiaRepository : INoticiaRepository
    {
        private readonly TPIContext context;

        public NoticiaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Noticia noticia)
        {
            context.Noticias.Add(noticia);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var noticia = await context.Noticias.FindAsync(id);
            if (noticia != null)
            {
                context.Noticias.Remove(noticia);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Noticia?> GetAsync(Guid id)
        {
            return await context.Noticias
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Noticia>> GetAllAsync()
        {
            return await context.Noticias
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Noticia noticia)
        {
            var existingNoticia = await context.Noticias.FindAsync(noticia.Id);
            if (existingNoticia != null)
            {
                existingNoticia.Titulo = noticia.Titulo;
                existingNoticia.Contenido = noticia.Contenido;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}