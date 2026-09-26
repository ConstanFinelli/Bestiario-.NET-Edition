using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly IRegistroRepository registroRepository;

        public RegistroService(IRegistroRepository registroRepository)
        {
            this.registroRepository = registroRepository;
        }

        public async Task<IEnumerable<RegistroDTO>> GetByBestiaAsync(Guid idBestia)
        {
            var registros = await registroRepository.GetByBestiaAsync(idBestia);
            return registros.Select(r => MapToDTO(r)).ToList();
        }

        public async Task<RegistroDTO?> GetAsync(Guid idBestia, int nroRegistro)
        {
            var registro = await registroRepository.GetAsync(idBestia, nroRegistro);
            return registro != null ? MapToDTO(registro) : null;
        }

        public async Task<RegistroDTO> AddAsync(RegistroDTO dto)
        {
            var registro = new Registro(
                dto.IdBestia,
                dto.NroRegistro,
                dto.IdUsuarioPublicador,
                dto.IdInvestigadorAprobador,
                dto.FechaAprobacion,
                dto.FechaBaja,
                string.IsNullOrWhiteSpace(dto.Estado) ? "pendiente" : dto.Estado
            );

            if (dto.Contenidos != null)
            {
                foreach (var c in dto.Contenidos)
                {
                    registro.AgregarContenido(c.Titulo, c.Contenido);
                }
            }

            await registroRepository.AddAsync(registro);

            return MapToDTO(registro);
        }

        public async Task<bool> UpdateAsync(RegistroDTO dto)
        {
            var registro = new Registro(
                dto.IdBestia,
                dto.NroRegistro,
                dto.IdUsuarioPublicador,
                dto.IdInvestigadorAprobador,
                dto.FechaAprobacion,
                dto.FechaBaja,
                dto.Estado
            );

            if (dto.Contenidos != null)
            {
                foreach (var c in dto.Contenidos)
                {
                    registro.AgregarContenido(c.Titulo, c.Contenido);
                }
            }

            return await registroRepository.UpdateAsync(registro);
        }

        public async Task<bool> DeleteAsync(Guid idBestia, int nroRegistro)
        {
            return await registroRepository.DeleteAsync(idBestia, nroRegistro);
        }

        public async Task<bool> AprobarAsync(Guid idBestia, int nroRegistro, Guid idInvestigador)
        {
            return await registroRepository.AprobarAsync(idBestia, nroRegistro, idInvestigador);
        }

        private static RegistroDTO MapToDTO(Registro r)
        {
            return new RegistroDTO
            {
                IdBestia = r.IdBestia,
                NroRegistro = r.NroRegistro,
                FechaAprobacion = r.FechaAprobacion,
                FechaBaja = r.FechaBaja,
                IdUsuarioPublicador = r.IdUsuarioPublicador,
                CorreoPublicador = r.UsuarioPublicador?.Correo,
                IdInvestigadorAprobador = r.IdInvestigadorAprobador,
                NombreInvestigadorAprobador = r.InvestigadorAprobador != null
                    ? $"{r.InvestigadorAprobador.Nombre} {r.InvestigadorAprobador.Apellido}".Trim()
                    : null,
                Estado = r.Estado,
                Contenidos = r.Contenidos.Select(c => new ContenidoRegistroDTO
                {
                    IdBestia = c.IdBestia,
                    NroRegistro = c.NroRegistro,
                    NroContenido = c.NroContenido,
                    Titulo = c.Titulo,
                    Contenido = c.Contenido
                }).OrderBy(c => c.NroContenido).ToList()
            };
        }
    }
}
