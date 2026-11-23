using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITokenRecuperacionRepository tokenRecuperacionRepository;
        private readonly IEmailService emailService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            ITokenRecuperacionRepository tokenRecuperacionRepository,
            IEmailService emailService)
        {
            this.usuarioRepository = usuarioRepository;
            this.tokenRecuperacionRepository = tokenRecuperacionRepository;
            this.emailService = emailService;
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
            var existing = await usuarioRepository.GetByCorreoAsync(dto.Correo);
            if (existing != null)
            {
                throw new ArgumentException("El correo ya se encuentra registrado.");
            }

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
            var existing = await usuarioRepository.GetByCorreoAsync(dto.Correo);
            if (existing != null)
            {
                throw new ArgumentException("El correo ya se encuentra registrado.");
            }

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
            var existing = await usuarioRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            var existingByCorreo = await usuarioRepository.GetByCorreoAsync(dto.Correo);
            if (existingByCorreo != null && existingByCorreo.Id != dto.Id)
            {
                throw new ArgumentException("El correo ya se encuentra registrado.");
            }

            string contrasenia = string.IsNullOrWhiteSpace(dto.Contrasenia)
                ? existing.Contrasenia
                : dto.Contrasenia;

            Usuario usuario;
            if (dto is InvestigadorDTO invDto)
            {
                usuario = new Investigador(
                    invDto.Id,
                    invDto.Correo,
                    contrasenia,
                    invDto.Dni,
                    invDto.Nombre,
                    invDto.Apellido,
                    invDto.RecibirNotificaciones
                );
            }
            else if (dto is LectorDTO || existing is Lector)
            {
                usuario = new Lector(
                    dto.Id,
                    dto.Correo,
                    contrasenia,
                    dto.RecibirNotificaciones
                );
            }
            else
            {
                usuario = new Usuario(
                    dto.Id,
                    dto.Correo,
                    contrasenia,
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

        public async Task SolicitarRecuperacionContraseniaAsync(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            {
                throw new ArgumentException("Debe ingresar un correo electrónico válido.");
            }

            var usuario = await usuarioRepository.GetByCorreoAsync(correo.Trim());
            if (usuario == null)
            {
                return;
            }

            const int minutosExpiracion = 15;
            string codigo = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

            await tokenRecuperacionRepository.InvalidarPreviosAsync(usuario.Id);
            var token = new TokenRecuperacion(usuario.Id, HashCodigo(codigo), minutosExpiracion);
            await tokenRecuperacionRepository.AddAsync(token);

            await emailService.EnviarCodigoRecuperacionAsync(usuario.Correo, codigo, minutosExpiracion);
        }

        public async Task RestablecerContraseniaAsync(string correo, string codigo, string nuevaContrasenia)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                throw new ArgumentException("El correo electrónico es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException("El código de verificación es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(nuevaContrasenia))
            {
                throw new ArgumentException("La nueva contraseña no puede estar vacía.");
            }

            var usuario = await usuarioRepository.GetByCorreoAsync(correo.Trim());
            if (usuario == null)
            {
                throw new ArgumentException("El código de verificación es inválido o ha expirado.");
            }

            var token = await tokenRecuperacionRepository.GetVigenteAsync(usuario.Id, HashCodigo(codigo));
            if (token == null || !token.EstaVigente())
            {
                throw new ArgumentException("El código de verificación es inválido o ha expirado.");
            }

            usuario.SetContrasenia(nuevaContrasenia);
            await usuarioRepository.UpdateAsync(usuario);
            await tokenRecuperacionRepository.MarcarUsadoAsync(token.Id);
        }

        private static string HashCodigo(string codigo)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(codigo.Trim()));
            return Convert.ToHexString(bytes);
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
