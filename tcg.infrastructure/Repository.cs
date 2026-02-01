using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TCG.Application.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace TCG.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _db;

        //CAN I FIND A REFERENCE FOR ALL OF THESE???????

        async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        Task<T> GetByIdAsync<T>(int id) where T : class
        {

        }

        Task<T> GetByAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {

        }

        Task<List<T>> GetAllByAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {

        }

        Task<T> AddAsync<T>(T entity) where T : class
        {

        }

        Task<T> UpdateAsync<T>(T entity) where T : class
        {

        }

        Task<T> DeleteAsync<T>(T entity) where T : class
        {

        }
    }
}
