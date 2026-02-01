using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using TCG.Application.DTOs;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces
{
    public interface IRepository
    {
        Task<List<T>> GetAllAsync<T>() where T : class;

        Task<T> GetByIdAsync<T>(int id) where T : class;

        Task<T> GetByAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<List<T>> GetAllByAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> AddAsync<T>(T entity) where T : class;

        Task<T> UpdateAsync<T>(T entity) where T : class;

        Task<T> DeleteAsync<T>(T entity) where T : class;
    }
}
