using System.Collections.Generic;
using System.Threading.Tasks;

namespace TCG.Application.Interfaces
{
    public interface IGenericService<TDto>
        where TDto : class
    {
        Task<TDto> CreateAsync(TDto dto);

        Task<TDto?> GetByIdAsync(object id);

        Task<List<TDto>> GetAllAsync();
    }
}