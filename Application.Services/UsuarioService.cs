using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await usuarioRepository.GetAllAsync();
            return usuarios.Select(u => MapToDTO(u)).ToList();
        }

        public async Task<UsuarioDTO?> GetAsync(Guid id)
        {
            var u = await usuarioRepository.GetAsync(id);
            return u != null ? MapToDTO(u) : null;
        }

        public async Task<UsuarioDTO> AddInvestigadorAsync(InvestigadorDTO dto)
        {
            var inv = new Investigador(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.Correo,
                dto.Contrasenia ?? "password",
                dto.Dni,
                dto.Nombre,
                dto.Apellido,
                dto.RecibirNotificaciones
            );

            await usuarioRepository.AddAsync(inv);
            dto.Id = inv.Id;
            dto.TipoUsuario = "Investigador";
            return dto;
        }

        public async Task<UsuarioDTO> AddLectorAsync(LectorDTO dto)
        {
            var lec = new Lector(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.Correo,
                dto.Contrasenia ?? "password",
                dto.RecibirNotificaciones
            );

            await usuarioRepository.AddAsync(lec);
            dto.Id = lec.Id;
            dto.TipoUsuario = "Lector";
            return dto;
        }

        public async Task<bool> UpdateAsync(UsuarioDTO dto)
        {
            Usuario usuario;
            if (dto is InvestigadorDTO invDto)
            {
                usuario = new Investigador(
                    invDto.Id,
                    invDto.Correo,
                    invDto.Contrasenia ?? "",
                    invDto.Dni,
                    invDto.Nombre,
                    invDto.Apellido,
                    invDto.RecibirNotificaciones
                );
            }
            else
            {
                usuario = new Usuario(
                    dto.Id,
                    dto.Correo,
                    dto.Contrasenia ?? "",
                    dto.RecibirNotificaciones
                );
            }

            return await usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await usuarioRepository.DeleteAsync(id);
        }

        public async Task<UsuarioDTO?> ValidarCredencialesAsync(string correo, string contrasenia)
        {
            var u = await usuarioRepository.ValidarCredencialesAsync(correo, contrasenia);
            return u != null ? MapToDTO(u) : null;
        }

        private static UsuarioDTO MapToDTO(Usuario u)
        {
            if (u is Investigador inv)
            {
                return new InvestigadorDTO
                {
                    Id = inv.Id,
                    Correo = inv.Correo,
                    RecibirNotificaciones = inv.RecibirNotificaciones,
                    Dni = inv.Dni,
                    Nombre = inv.Nombre,
                    Apellido = inv.Apellido,
                    TipoUsuario = "Investigador"
                };
            }

            if (u is Lector lec)
            {
                return new LectorDTO
                {
                    Id = lec.Id,
                    Correo = lec.Correo,
                    RecibirNotificaciones = lec.RecibirNotificaciones,
                    TipoUsuario = "Lector"
                };
            }

            return new UsuarioDTO
            {
                Id = u.Id,
                Correo = u.Correo,
                RecibirNotificaciones = u.RecibirNotificaciones,
                TipoUsuario = "Usuario"
            };
        }
    }
}
