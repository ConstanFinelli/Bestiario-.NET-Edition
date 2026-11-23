using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly TPIContext context;

        public RegistroRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Registro>> GetByBestiaAsync(Guid idBestia)
        {
            return await context.Registros
                .Include(r => r.UsuarioPublicador)
                .Include(r => r.InvestigadorAprobador)
                .Include(r => r.Contenidos)
                .Where(r => r.IdBestia == idBestia)
                .OrderBy(r => r.NroRegistro)
                .ToListAsync();
        }

        public async Task<Registro?> GetAsync(Guid idBestia, int nroRegistro)
        {
            return await context.Registros
                .Include(r => r.UsuarioPublicador)
                .Include(r => r.InvestigadorAprobador)
                .Include(r => r.Contenidos)
                .FirstOrDefaultAsync(r => r.IdBestia == idBestia && r.NroRegistro == nroRegistro);
        }

        public async Task<Registro?> GetVigenteAsync(Guid idBestia, DateTime? fecha = null)
        {
            DateTime fechaConsulta = fecha ?? DateTime.Now;
            return await context.Registros
                .Include(r => r.UsuarioPublicador)
                .Include(r => r.InvestigadorAprobador)
                .Include(r => r.Contenidos)
                .Where(r => r.IdBestia == idBestia
                    && r.Estado == "aprobado"
                    && r.FechaAprobacion != null
                    && r.FechaAprobacion <= fechaConsulta
                    && (r.FechaBaja == null || r.FechaBaja > fechaConsulta))
                .OrderByDescending(r => r.FechaAprobacion)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetNextNroRegistroAsync(Guid idBestia)
        {
            var max = await context.Registros
                .Where(r => r.IdBestia == idBestia)
                .Select(r => (int?)r.NroRegistro)
                .MaxAsync();

            return (max ?? 0) + 1;
        }

        public async Task AddAsync(Registro registro)
        {
            if (registro.NroRegistro <= 0)
            {
                registro.NroRegistro = await GetNextNroRegistroAsync(registro.IdBestia);
            }

            if (string.IsNullOrWhiteSpace(registro.MainPicture))
            {
                var vigente = await GetVigenteAsync(registro.IdBestia);
                if (vigente != null && !string.IsNullOrWhiteSpace(vigente.MainPicture))
                {
                    registro.MainPicture = vigente.MainPicture;
                }
            }

            if (registro.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase))
            {
                DateTime ahora = registro.FechaAprobacion ?? DateTime.Now;
                registro.FechaAprobacion = ahora;

                var vigentesAnteriores = await context.Registros
                    .Where(r => r.IdBestia == registro.IdBestia && r.Estado == "aprobado" && r.FechaBaja == null)
                    .ToListAsync();

                foreach (var ant in vigentesAnteriores)
                {
                    ant.FechaBaja = ahora;
                }
            }

            int nroContenido = 1;
            foreach (var cont in registro.Contenidos)
            {
                cont.IdBestia = registro.IdBestia;
                cont.NroRegistro = registro.NroRegistro;
                cont.NroContenido = nroContenido++;
            }

            context.Registros.Add(registro);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Registro registro)
        {
            var existing = await context.Registros
                .Include(r => r.Contenidos)
                .FirstOrDefaultAsync(r => r.IdBestia == registro.IdBestia && r.NroRegistro == registro.NroRegistro);

            if (existing == null)
                return false;

            existing.SetEstado(registro.Estado);
            existing.FechaAprobacion = registro.FechaAprobacion;
            existing.FechaBaja = registro.FechaBaja;
            existing.IdUsuarioPublicador = registro.IdUsuarioPublicador;
            existing.IdInvestigadorAprobador = registro.IdInvestigadorAprobador;
            if (!string.IsNullOrWhiteSpace(registro.MainPicture))
            {
                existing.MainPicture = registro.MainPicture;
            }

            // Reemplazo maestro/detalle de contenidos
            context.ContenidoRegistros.RemoveRange(existing.Contenidos);

            int nroContenido = 1;
            foreach (var cont in registro.Contenidos)
            {
                existing.Contenidos.Add(new ContenidoRegistro(
                    existing.IdBestia,
                    existing.NroRegistro,
                    nroContenido++,
                    cont.Titulo,
                    cont.Contenido
                ));
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid idBestia, int nroRegistro)
        {
            var registro = await context.Registros
                .Include(r => r.Contenidos)
                .FirstOrDefaultAsync(r => r.IdBestia == idBestia && r.NroRegistro == nroRegistro);

            if (registro != null)
            {
                context.Registros.Remove(registro);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AprobarAsync(Guid idBestia, int nroRegistro, Guid idInvestigador)
        {
            var registro = await context.Registros.FirstOrDefaultAsync(r => r.IdBestia == idBestia && r.NroRegistro == nroRegistro);
            if (registro == null)
                return false;

            DateTime ahora = DateTime.Now;
            var vigentesAnteriores = await context.Registros
                .Where(r => r.IdBestia == idBestia
                    && r.NroRegistro != nroRegistro
                    && r.Estado == "aprobado"
                    && r.FechaBaja == null)
                .ToListAsync();

            foreach (var ant in vigentesAnteriores)
            {
                ant.FechaBaja = ahora;
            }

            registro.Aprobar(idInvestigador);
            registro.FechaAprobacion = ahora;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
