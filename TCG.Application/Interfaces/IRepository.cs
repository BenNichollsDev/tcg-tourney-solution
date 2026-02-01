using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();

        Task<List<TDto>> GetAllProjectedAsync<TDto>() where TDto : class;

        Task<T?> GetByIdAsync(object id);

        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>?> GetAllByAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}