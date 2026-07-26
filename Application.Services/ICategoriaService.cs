using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaDTO> AddAsync(CategoriaDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<CategoriaDTO?> GetAsync(Guid id);
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();
        Task<bool> UpdateAsync(CategoriaDTO dto);
    }
}
