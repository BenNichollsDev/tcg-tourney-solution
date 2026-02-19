using AutoMapper;
using AutoMapper.QueryableExtensions;
// REF AUTOMAPPER 01/02/2026

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TCG.Application.Interfaces;

namespace TCG.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _db;
        private readonly IMapper _mapper;

        // CAN I FIND A REFERENCE FOR ALL OF THESE???????
        public Repository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IQueryable<T> Query() => _db.Set<T>();

        // Generic projection using AutoMapper
        public async Task<List<TDto>> GetAllProjectedAsync<TDto>() where TDto : class
        {
            return await _db.Set<T>()
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
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

        public async Task<T> AddAsync(T dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _db.AddAsync(entity);
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

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}