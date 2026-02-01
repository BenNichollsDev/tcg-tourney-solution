using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using TCG.Application.DTOs;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>?> GetAllAsync();

        Task<T?> GetByIdAsync(object id);

        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>?> GetAllByAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}
