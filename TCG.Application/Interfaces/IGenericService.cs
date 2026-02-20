using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TCG.Application.Interfaces
{
    public interface IGenericService<TEntity, TDto>
        where TDto : class
    {
        Task<TDto> CreateAsync(TDto dto);

        Task<TDto?> GetByIdAsync(object id);

        Task<List<TDto>> GetAllAsync();

        Task<TDto?> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}