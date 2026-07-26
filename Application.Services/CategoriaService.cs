using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTOs;
using Domain.Model;

namespace Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaDTO> AddAsync(CategoriaDTO dto)
        {
            Categoria categoria = new Categoria(Guid.NewGuid(), dto.Nombre, dto.Descripcion);

            await categoriaRepository.AddAsync(categoria);

            dto.Id = categoria.Id;
            dto.Descripcion = categoria.Descripcion;
            dto.Nombre = categoria.Nombre;

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await categoriaRepository.DeleteAsync(id);
        }

        public async Task<CategoriaDTO?> GetAsync(Guid id)
        {
            Categoria? categoria = await categoriaRepository.GetAsync(id);

            if (categoria == null)
                return null;

            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await categoriaRepository.GetAllAsync();

            return categorias.Select(categoria => new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            }).ToList();
        }

        public async Task<bool> UpdateAsync(CategoriaDTO dto)
        {
            var existing = await categoriaRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            Categoria categoria = new Categoria(dto.Id, dto.Nombre, dto.Descripcion);
            return await categoriaRepository.UpdateAsync(categoria);
        }

    }
}
