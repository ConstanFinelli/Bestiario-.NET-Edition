using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly TPIContext context;

        public NoticiaRepository(TPIContext context)
        {
            this.context = context;
        }

        private async Task EnsurePublicadorColumnExistsAsync()
        {
            const string sql = @"
IF COL_LENGTH(N'Noticias', N'PublicadorId') IS NULL
BEGIN
    ALTER TABLE [Noticias] ADD [PublicadorId] uniqueidentifier NULL;
    CREATE INDEX [IX_Noticias_PublicadorId] ON [Noticias] ([PublicadorId]);
    ALTER TABLE [Noticias] ADD CONSTRAINT [FK_Noticias_Usuarios_PublicadorId]
        FOREIGN KEY ([PublicadorId]) REFERENCES [Usuarios] ([Id]) ON DELETE SET NULL;
END";
            await context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task AddAsync(Noticia noticia)
        {
            await EnsurePublicadorColumnExistsAsync();

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
            await EnsurePublicadorColumnExistsAsync();

            return await context.Noticias
                .Include(n => n.Publicador)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Noticia>> GetAllAsync()
        {
            await EnsurePublicadorColumnExistsAsync();

            return await context.Noticias
                .Include(n => n.Publicador)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Noticia noticia)
        {
            await EnsurePublicadorColumnExistsAsync();

            var existingNoticia = await context.Noticias.FindAsync(noticia.Id);
            if (existingNoticia != null)
            {
                existingNoticia.Titulo = noticia.Titulo;
                existingNoticia.Contenido = noticia.Contenido;
                if (noticia.PublicadorId.HasValue)
                {
                    existingNoticia.PublicadorId = noticia.PublicadorId;
                }

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
