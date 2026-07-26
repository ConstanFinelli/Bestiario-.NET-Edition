using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface INoticiaService
    {
        Task<NoticiaDTO> AddAsync(NoticiaDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<NoticiaDTO?> GetAsync(Guid id);
        Task<IEnumerable<NoticiaDTO>> GetAllAsync();
        Task<bool> UpdateAsync(NoticiaDTO dto);
    }
}
