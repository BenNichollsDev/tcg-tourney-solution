using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCG.Application.Interfaces;

namespace TCG.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly AppDbContext _db;

        public Repository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<TEntity> Query()
            => _db.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(object id)
            => await _db.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate)
            => await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate)
            => await _db.Set<TEntity>()
                        .AsNoTracking()
                        .Where(predicate)
                        .ToListAsync();

        public async Task AddAsync(TEntity entity)
            => await _db.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => _db.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _db.Set<TEntity>().Remove(entity);

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }
}





//public async Task<List<TDto>> GetAllProjectedAsync<TDto>() where TDto : class
//{
//    return await _db.Set<T>()
//        .ProjectTo<TDto>(_mapper.ConfigurationProvider)
//        .ToListAsync();
//}

//public async Task<T?> GetByIdAsync(object id)
//{
//    return await _db.Set<T>()
//        .FindAsync(id);
//}

//public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate)
//{
//    return await _db.Set<T>()
//        .FirstOrDefaultAsync(predicate);
//}

//public async Task<List<T>?> GetAllByAsync(Expression<Func<T, bool>> predicate)
//{
//    return await _db.Set<T>()
//        .AsNoTracking()
//        .Where(predicate)
//        .ToListAsync();
//}

//public async Task<T> AddAsync(T entity)
//{
//    _db.Set<T>().Add(entity);
//    await _db.SaveChangesAsync();
//    return entity;
//}

//public async Task<T> UpdateAsync(T entity)
//{
//    _db.Set<T>().Update(entity);
//    await _db.SaveChangesAsync();
//    return entity;
//}

//public async Task<T> DeleteAsync(T entity)
//{
//    _db.Set<T>().Remove(entity);
//    await _db.SaveChangesAsync();
//    return entity;
//}