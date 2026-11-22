using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SolicitudInvestigadorRepository : ISolicitudInvestigadorRepository
    {
        private readonly TPIContext context;

        public SolicitudInvestigadorRepository(TPIContext context)
        {
            this.context = context;
        }

        private async Task EnsureTableExistsAsync()
        {
            const string sql = @"
IF OBJECT_ID(N'[SolicitudesInvestigador]', N'U') IS NULL
BEGIN
    CREATE TABLE [SolicitudesInvestigador] (
        [Id] uniqueidentifier NOT NULL,
        [IdLector] uniqueidentifier NOT NULL,
        [Dni] nvarchar(20) NOT NULL,
        [Nombre] nvarchar(50) NOT NULL,
        [Apellido] nvarchar(50) NOT NULL,
        [Motivo] nvarchar(500) NOT NULL,
        [Estado] nvarchar(20) NOT NULL,
        [FechaSolicitud] datetime2 NOT NULL,
        [FechaResolucion] datetime2 NULL,
        [IdInvestigadorResolutor] uniqueidentifier NULL,
        CONSTRAINT [PK_SolicitudesInvestigador] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SolicitudesInvestigador_Usuarios_IdLector] FOREIGN KEY ([IdLector]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SolicitudesInvestigador_Usuarios_IdInvestigadorResolutor] FOREIGN KEY ([IdInvestigadorResolutor]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
    );
    CREATE INDEX [IX_SolicitudesInvestigador_IdLector] ON [SolicitudesInvestigador] ([IdLector]);
    CREATE INDEX [IX_SolicitudesInvestigador_IdInvestigadorResolutor] ON [SolicitudesInvestigador] ([IdInvestigadorResolutor]);
END";
            await context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task<IEnumerable<SolicitudInvestigador>> GetAllAsync(string? estado = null)
        {
            await EnsureTableExistsAsync();

            var query = context.SolicitudesInvestigador
                .Include(s => s.Lector)
                .Include(s => s.InvestigadorResolutor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado))
            {
                string est = estado.Trim().ToLowerInvariant();
                query = query.Where(s => s.Estado.ToLower() == est);
            }

            return await query
                .OrderByDescending(s => s.FechaSolicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<SolicitudInvestigador>> GetByLectorAsync(Guid idLector)
        {
            await EnsureTableExistsAsync();

            return await context.SolicitudesInvestigador
                .Include(s => s.Lector)
                .Include(s => s.InvestigadorResolutor)
                .Where(s => s.IdLector == idLector)
                .OrderByDescending(s => s.FechaSolicitud)
                .ToListAsync();
        }

        public async Task<SolicitudInvestigador?> GetAsync(Guid id)
        {
            await EnsureTableExistsAsync();

            return await context.SolicitudesInvestigador
                .Include(s => s.Lector)
                .Include(s => s.InvestigadorResolutor)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> HasPendienteAsync(Guid idLector)
        {
            await EnsureTableExistsAsync();

            return await context.SolicitudesInvestigador
                .AnyAsync(s => s.IdLector == idLector && s.Estado == "pendiente");
        }

        public async Task AddAsync(SolicitudInvestigador solicitud)
        {
            await EnsureTableExistsAsync();

            context.SolicitudesInvestigador.Add(solicitud);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AprobarAsync(Guid idSolicitud, Guid idInvestigador)
        {
            await EnsureTableExistsAsync();

            await using var transaction = await context.Database.BeginTransactionAsync();

            var solicitud = await context.SolicitudesInvestigador
                .FirstOrDefaultAsync(s => s.Id == idSolicitud);

            if (solicitud == null)
            {
                return false;
            }

            solicitud.Aprobar(idInvestigador);

            // Promoción TPH de Lector a Investigador preservando el Id original y sus FKs existentes
            await context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE Usuarios
SET TipoUsuario = 'Investigador',
    Dni = {solicitud.Dni},
    Nombre = {solicitud.Nombre},
    Apellido = {solicitud.Apellido}
WHERE Id = {solicitud.IdLector}");

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }

        public async Task<bool> RechazarAsync(Guid idSolicitud, Guid idInvestigador)
        {
            await EnsureTableExistsAsync();

            var solicitud = await context.SolicitudesInvestigador
                .FirstOrDefaultAsync(s => s.Id == idSolicitud);

            if (solicitud == null)
            {
                return false;
            }

            solicitud.Rechazar(idInvestigador);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
