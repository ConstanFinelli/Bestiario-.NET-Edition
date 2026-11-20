using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TipoEvidenciaRepository : ITipoEvidenciaRepository
    {
        private readonly TPIContext context;

        public TipoEvidenciaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TipoEvidencia>> GetAllAsync()
        {
            return await context.TiposEvidencia.ToListAsync();
        }

        public async Task<TipoEvidencia?> GetAsync(Guid id)
        {
            return await context.TiposEvidencia.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TipoEvidencia tipoEvidencia)
        {
            context.TiposEvidencia.Add(tipoEvidencia);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TipoEvidencia tipoEvidencia)
        {
            var existing = await context.TiposEvidencia.FindAsync(tipoEvidencia.Id);
            if (existing == null)
                return false;

            existing.SetDescripcion(tipoEvidencia.Descripcion);
            existing.SetResourceType(tipoEvidencia.ResourceType);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await context.TiposEvidencia.FindAsync(id);
            if (existing != null)
            {
                context.TiposEvidencia.Remove(existing);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

