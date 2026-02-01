using System.Linq.Expressions;
using TCG.Application.Interfaces;
using AutoMapper;

namespace TCG.Application.Services
{
    public class Service<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET ALL using AutoMapper projection
        public async Task<List<TDto>> GetAllAsync()
        {
            return await _repository.GetAllProjectedAsync<TDto>();
        }

        // GET BY ID
        public async Task<TDto?> GetByIdAsync(object id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        // GET BY PREDICATE
        public async Task<TDto?> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await _repository.GetByAsync(predicate);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        // GET ALL BY PREDICATE
        public async Task<List<TDto>?> GetAllByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _repository.GetAllByAsync(predicate);
            return entities.Select(e => _mapper.Map<TDto>(e)).ToList();
        }

        // ADD ENTITY
        public async Task<TDto> AddAsync(TDto Dto)
        {
            var entity = _mapper.Map<TEntity>(Dto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(result);
        }

        // UPDATE ENTITY
        public async Task<TDto> UpdateAsync(TDto Dto)
        {
            var entity = _mapper.Map<TEntity>(Dto);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<TDto>(result);
        }

        // DELETE ENTITY
        public async Task DeleteAsync(TDto Dto)
        {
            var entity = _mapper.Map<TEntity>(Dto);
            await _repository.DeleteAsync(entity);
        }
    }
}