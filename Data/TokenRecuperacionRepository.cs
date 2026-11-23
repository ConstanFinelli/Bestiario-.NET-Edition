using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TokenRecuperacionRepository : ITokenRecuperacionRepository
    {
        private readonly TPIContext context;

        public TokenRecuperacionRepository(TPIContext context)
        {
            this.context = context;
        }

        private async Task EnsureTableExistsAsync()
        {
            const string sql = @"
IF OBJECT_ID(N'[TokensRecuperacion]', N'U') IS NULL
BEGIN
    CREATE TABLE [TokensRecuperacion] (
        [Id] uniqueidentifier NOT NULL,
        [IdUsuario] uniqueidentifier NOT NULL,
        [CodigoHash] nvarchar(64) NOT NULL,
        [FechaCreacion] datetime2 NOT NULL,
        [FechaExpiracion] datetime2 NOT NULL,
        [Usado] bit NOT NULL,
        CONSTRAINT [PK_TokensRecuperacion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TokensRecuperacion_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_TokensRecuperacion_IdUsuario] ON [TokensRecuperacion] ([IdUsuario]);
END";
            await context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task InvalidarPreviosAsync(Guid idUsuario)
        {
            await EnsureTableExistsAsync();

            var previos = await context.TokensRecuperacion
                .Where(t => t.IdUsuario == idUsuario && !t.Usado)
                .ToListAsync();

            foreach (var token in previos)
            {
                token.MarcarUsado();
            }

            if (previos.Count > 0)
            {
                await context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(TokenRecuperacion token)
        {
            await EnsureTableExistsAsync();

            context.TokensRecuperacion.Add(token);
            await context.SaveChangesAsync();
        }

        public async Task<TokenRecuperacion?> GetVigenteAsync(Guid idUsuario, string codigoHash)
        {
            await EnsureTableExistsAsync();

            var now = DateTime.UtcNow;
            return await context.TokensRecuperacion
                .Where(t => t.IdUsuario == idUsuario && t.CodigoHash == codigoHash && !t.Usado && t.FechaExpiracion >= now)
                .OrderByDescending(t => t.FechaCreacion)
                .FirstOrDefaultAsync();
        }

        public async Task MarcarUsadoAsync(Guid idToken)
        {
            await EnsureTableExistsAsync();

            var token = await context.TokensRecuperacion.FindAsync(idToken);
            if (token != null)
            {
                token.MarcarUsado();
                await context.SaveChangesAsync();
            }
        }
    }
}
