using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TCG.Application.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Query();

        Task<TEntity?> GetByIdAsync(int id);

        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}