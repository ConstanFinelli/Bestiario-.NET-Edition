using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly TPIContext context;

        public CategoriaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Categoria categoria)
        {
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var categoria = await context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Categoria?> GetAsync(Guid id)
        {
            return await context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await context.Categorias
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            var existingCategoria = await context.Categorias.FindAsync(categoria.Id);
            if (existingCategoria != null)
            {
                existingCategoria.SetNombre(categoria.Nombre);
                existingCategoria.SetDescripcion(categoria.Descripcion);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
