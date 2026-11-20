using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly TPIContext context;

        public ComentarioRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync()
        {
            return await context.Comentarios
                .Include(c => c.Bestia)
                .Include(c => c.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetByBestiaAsync(Guid idBestia)
        {
            return await context.Comentarios
                .Include(c => c.Bestia)
                .Include(c => c.Usuario)
                .Where(c => c.IdBestia == idBestia)
                .ToListAsync();
        }

        public async Task<Comentario?> GetAsync(Guid id)
        {
            return await context.Comentarios
                .Include(c => c.Bestia)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Comentario comentario)
        {
            context.Comentarios.Add(comentario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Comentario comentario)
        {
            var existing = await context.Comentarios.FindAsync(comentario.Id);
            if (existing == null)
                return false;

            existing.SetTitulo(comentario.Titulo);
            existing.SetContenido(comentario.Contenido);
            existing.SetEstado(comentario.Estado);
            existing.FechaAprobacion = comentario.FechaAprobacion;
            existing.IdBestia = comentario.IdBestia;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await context.Comentarios.FindAsync(id);
            if (existing != null)
            {
                context.Comentarios.Remove(existing);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AprobarAsync(Guid id)
        {
            var existing = await context.Comentarios.FindAsync(id);
            if (existing == null)
                return false;

            existing.Aprobar();
            await context.SaveChangesAsync();
            return true;
        }
    }
}

