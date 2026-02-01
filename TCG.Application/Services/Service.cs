using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TCG.Application.Interfaces;

namespace TCG.Application.Services
{
    public class Service<TEntity, TDTO> 
        where TEntity : class
        where TDTO : class, new()
    {
        private readonly IRepository<TEntity> _repository;
        private readonly Func<TEntity, TDTO> _mappedEntities;

        public Service(IRepository<TEntity> repository, Func<TEntity, TDTO> map)
        {
            _repository = repository;
            _mappedEntities = map;
        }

        public async Task<List<TDTO>> GetAllAsync()
        {
            var _entities = await _repository.GetAllAsync();
            return _entities.Select(e => _mappedEntities(e)).ToList();
        }

        public async Task<TDTO?> GetByIdAsync(object id)
        {
            var _entity = await _repository.GetByIdAsync(id);
            return _entity == null ? null : _mappedEntities(_entity);
        }

        public async Task<TDTO?> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var _entity = await _repository.GetByAsync(predicate);
            return _entity == null ? null : _mappedEntities(_entity);
        }

        public async Task<List<TDTO>?> GetAllByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var _entities = await _repository.GetAllByAsync(predicate);
            return _entities.Select(e => _mappedEntities(e)).ToList();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await _repository.DeleteAsync(entity);
        }
    }
}
