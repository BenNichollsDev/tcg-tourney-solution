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
        public readonly AppDbContext _db;

        //CAN I FIND A REFERENCE FOR ALL OF THESE???????

        public async Task<List<T>?> GetAllAsync<T>() where T : class
        {
            return await _db.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await _db.Set<T>()
                .FindAsync(id);
        }

        public async Task<T?> GetByAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _db.Set<T>()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>?> GetAllByAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _db.Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync<T>(T entity) where T : class
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
