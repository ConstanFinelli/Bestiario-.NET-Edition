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

        private async Task EnsureRecibirNotificacionesRemovedAsync()
        {
            const string sql = @"
IF COL_LENGTH(N'Usuarios', N'RecibirNotificaciones') IS NOT NULL
BEGIN
    DECLARE @constraintName nvarchar(128);
    SELECT @constraintName = dc.name
    FROM sys.default_constraints dc
    INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    WHERE dc.parent_object_id = OBJECT_ID(N'Usuarios')
      AND c.name = N'RecibirNotificaciones';

    IF @constraintName IS NOT NULL
        EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @constraintName + N']');

    ALTER TABLE [Usuarios] DROP COLUMN [RecibirNotificaciones];
END";
            await context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await EnsureRecibirNotificacionesRemovedAsync();

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
