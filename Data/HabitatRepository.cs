using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HabitatRepository : IHabitatRepository
    {
        private readonly TPIContext context;

        public HabitatRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Habitat>> GetAllAsync()
        {
            return await context.Habitats
                .Include(h => h.Caracteristicas)
                .ToListAsync();
        }

        public async Task<Habitat?> GetAsync(Guid id)
        {
            return await context.Habitats
                .Include(h => h.Caracteristicas)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(Habitat habitat)
        {
            int nro = 1;
            foreach (var c in habitat.Caracteristicas)
            {
                c.IdHabitat = habitat.Id;
                c.NroCaracteristica = nro++;
            }

            context.Habitats.Add(habitat);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Habitat habitat)
        {
            var existing = await context.Habitats
                .Include(h => h.Caracteristicas)
                .FirstOrDefaultAsync(h => h.Id == habitat.Id);

            if (existing == null)
                return false;

            existing.SetNombre(habitat.Nombre);
            existing.SetLocalizacion(habitat.Localizacion);
            existing.SetLatitud(habitat.Latitud);
            existing.SetLongitud(habitat.Longitud);

            context.CaracteristicasHabitat.RemoveRange(existing.Caracteristicas);

            int nro = 1;
            foreach (var c in habitat.Caracteristicas)
            {
                existing.Caracteristicas.Add(new CaracteristicaHabitat(
                    existing.Id,
                    nro++,
                    c.Descripcion
                ));
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var habitat = await context.Habitats
                .Include(h => h.Caracteristicas)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (habitat != null)
            {
                context.Habitats.Remove(habitat);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
