using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TPIContext context;

        public UsuarioRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetAsync(Guid id)
        {
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task AddAsync(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            var existing = await context.Usuarios.FindAsync(usuario.Id);
            if (existing == null)
                return false;

            existing.SetCorreo(usuario.Correo);
            existing.SetContrasenia(usuario.Contrasenia);
            existing.RecibirNotificaciones = usuario.RecibirNotificaciones;

            if (existing is Investigador exInv && usuario is Investigador inInv)
            {
                exInv.SetDni(inInv.Dni);
                exInv.SetNombre(inInv.Nombre);
                exInv.SetApellido(inInv.Apellido);
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await context.Usuarios.FindAsync(id);
            if (user != null)
            {
                context.Usuarios.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Usuario?> ValidarCredencialesAsync(string correo, string contrasenia)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contrasenia == contrasenia);
        }
    }
}
