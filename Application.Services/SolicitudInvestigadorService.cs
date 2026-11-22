using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class SolicitudInvestigadorService : ISolicitudInvestigadorService
    {
        private readonly ISolicitudInvestigadorRepository solicitudRepository;
        private readonly IUsuarioRepository usuarioRepository;

        public SolicitudInvestigadorService(
            ISolicitudInvestigadorRepository solicitudRepository,
            IUsuarioRepository usuarioRepository)
        {
            this.solicitudRepository = solicitudRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<SolicitudInvestigadorDTO>> GetAllAsync(string? estado = null)
        {
            var items = await solicitudRepository.GetAllAsync(estado);
            return items.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<SolicitudInvestigadorDTO>> GetByLectorAsync(Guid idLector)
        {
            var items = await solicitudRepository.GetByLectorAsync(idLector);
            return items.Select(MapToDTO).ToList();
        }

        public async Task<SolicitudInvestigadorDTO> CrearSolicitudAsync(Guid idLector, CrearSolicitudInvestigadorDTO dto)
        {
            var usuario = await usuarioRepository.GetAsync(idLector);
            if (usuario == null)
            {
                throw new ArgumentException("El usuario solicitante no existe.");
            }

            if (usuario is Investigador)
            {
                throw new ArgumentException("El usuario ya posee el rol de Investigador.");
            }

            if (await solicitudRepository.HasPendienteAsync(idLector))
            {
                throw new ArgumentException("Ya tenés una solicitud pendiente de revisión.");
            }

            var solicitud = new SolicitudInvestigador(
                idLector,
                dto.Dni,
                dto.Nombre,
                dto.Apellido,
                dto.Motivo
            );

            await solicitudRepository.AddAsync(solicitud);
            var created = await solicitudRepository.GetAsync(solicitud.Id);
            return created != null ? MapToDTO(created) : MapToDTO(solicitud);
        }

        public async Task<bool> AprobarAsync(Guid idSolicitud, Guid idInvestigador)
        {
            return await solicitudRepository.AprobarAsync(idSolicitud, idInvestigador);
        }

        public async Task<bool> RechazarAsync(Guid idSolicitud, Guid idInvestigador)
        {
            return await solicitudRepository.RechazarAsync(idSolicitud, idInvestigador);
        }

        private static SolicitudInvestigadorDTO MapToDTO(SolicitudInvestigador s)
        {
            return new SolicitudInvestigadorDTO
            {
                Id = s.Id,
                IdLector = s.IdLector,
                CorreoLector = s.Lector?.Correo,
                Dni = s.Dni,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Motivo = s.Motivo,
                Estado = s.Estado,
                FechaSolicitud = s.FechaSolicitud,
                FechaResolucion = s.FechaResolucion,
                IdInvestigadorResolutor = s.IdInvestigadorResolutor,
                NombreInvestigadorResolutor = s.InvestigadorResolutor != null
                    ? $"{s.InvestigadorResolutor.Nombre} {s.InvestigadorResolutor.Apellido}".Trim()
                    : null
            };
        }
    }
}
