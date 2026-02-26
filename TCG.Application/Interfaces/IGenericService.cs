using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TCG.Application.Interfaces
{
    public interface IGenericService<TEntity, TDto>
        where TDto : class
    {
        public Task<TDto> AddAsync(TDto dto);
        
        public Task<TDto> UpdateAsync(TDto dto);
        
        public Task<TDto?> DeleteAsync(TDto dto);
        
        public Task<TDto?> DeleteAsync(int id);

        public Task<TDto?> GetByIdAsync(int id);

        public Task<List<TDto>> GetAllAsync();

        public Task<TDto?> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}