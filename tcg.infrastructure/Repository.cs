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