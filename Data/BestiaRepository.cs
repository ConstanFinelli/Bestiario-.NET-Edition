using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BestiaRepository : IBestiaRepository
    {
        private readonly TPIContext context;

        public BestiaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Bestia>> GetAllAsync()
        {
            return await context.Bestias
                .Include(b => b.Categorias)
                .ToListAsync();
        }

        public async Task<Bestia?> GetAsync(Guid id)
        {
            return await context.Bestias
                .Include(b => b.Categorias)
                .Include(b => b.Registros)
                    .ThenInclude(r => r.Contenidos)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Bestia bestia, IEnumerable<Guid>? categoriaIds = null)
        {
            if (categoriaIds != null && categoriaIds.Any())
            {
                var categorias = await context.Categorias
                    .Where(c => categoriaIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var cat in categorias)
                {
                    bestia.Categorias.Add(cat);
                }
            }

            context.Bestias.Add(bestia);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Bestia bestia, IEnumerable<Guid>? categoriaIds = null)
        {
            var existing = await context.Bestias
                .Include(b => b.Categorias)
                .FirstOrDefaultAsync(b => b.Id == bestia.Id);

            if (existing == null)
                return false;

            existing.SetNombre(bestia.Nombre);
            existing.SetPeligrosidad(bestia.Peligrosidad);
            existing.SetEstado(bestia.Estado);

            if (categoriaIds != null)
            {
                existing.Categorias.Clear();
                var categorias = await context.Categorias
                    .Where(c => categoriaIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var cat in categorias)
                {
                    existing.Categorias.Add(cat);
                }
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var bestia = await context.Bestias.FindAsync(id);
            if (bestia != null)
            {
                context.Bestias.Remove(bestia);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
