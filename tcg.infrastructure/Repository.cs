using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TCG.Application.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace TCG.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _db;

        //CAN I FIND A REFERENCE FOR ALL OF THESE???????

        public async Task<List<T>?> GetAllAsync()
        {
            return await _db.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _db.Set<T>()
                .FindAsync(id);
        }

        public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>?> GetAllByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
