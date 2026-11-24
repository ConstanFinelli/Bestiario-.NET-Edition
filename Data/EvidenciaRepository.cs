using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EvidenciaRepository : IEvidenciaRepository
    {
        private readonly TPIContext context;

        public EvidenciaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Evidencia>> GetByBestiaAsync(Guid idBestia)
        {
            return await context.Evidencias
                .Include(e => e.TipoEvidencia)
                .Where(e => e.IdBestia == idBestia)
                .OrderBy(e => e.NroEvidencia)
                .ToListAsync();
        }

        public async Task<Evidencia?> GetAsync(Guid idBestia, int nroEvidencia)
        {
            return await context.Evidencias
                .Include(e => e.TipoEvidencia)
                .FirstOrDefaultAsync(e => e.IdBestia == idBestia && e.NroEvidencia == nroEvidencia);
        }

        private async Task<int> GetNextNroEvidenciaAsync(Guid idBestia)
        {
            var max = await context.Evidencias
                .Where(e => e.IdBestia == idBestia)
                .Select(e => (int?)e.NroEvidencia)
                .MaxAsync();

            return (max ?? 0) + 1;
        }

        public async Task AddAsync(Evidencia evidencia)
        {
            if (evidencia.NroEvidencia <= 0)
            {
                evidencia.NroEvidencia = await GetNextNroEvidenciaAsync(evidencia.IdBestia);
            }

            context.Evidencias.Add(evidencia);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Evidencia evidencia)
        {
            var existing = await context.Evidencias
                .FirstOrDefaultAsync(e => e.IdBestia == evidencia.IdBestia && e.NroEvidencia == evidencia.NroEvidencia);

            if (existing == null)
                return false;

            existing.SetEstado(evidencia.Estado);
            existing.FechaObtencion = evidencia.FechaObtencion;
            existing.SetFileId(evidencia.FileId);
            existing.IdTipoEvidencia = evidencia.IdTipoEvidencia;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid idBestia, int nroEvidencia)
        {
            var existing = await context.Evidencias
                .FirstOrDefaultAsync(e => e.IdBestia == idBestia && e.NroEvidencia == nroEvidencia);

            if (existing != null)
            {
                context.Evidencias.Remove(existing);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AprobarAsync(Guid idBestia, int nroEvidencia)
        {
            var existing = await context.Evidencias
                .FirstOrDefaultAsync(e => e.IdBestia == idBestia && e.NroEvidencia == nroEvidencia);

            if (existing == null)
                return false;

            existing.Aprobar();
            await context.SaveChangesAsync();
            return true;
        }
    }
}

