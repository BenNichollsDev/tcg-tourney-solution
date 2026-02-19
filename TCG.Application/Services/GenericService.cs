using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCG.Application.Interfaces;

namespace TCG.Application.Services
{
    public class GenericService<TEntity, TDto>
        : IGenericService<TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(
            IRepository<TEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto?> GetByIdAsync(object id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return entity == null
                ? null
                : _mapper.Map<TDto>(entity);
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _repository.Query()
                                            .AsNoTracking()
                                            .ToListAsync();

            return _mapper.Map<List<TDto>>(entities);
        }

        //CHANGE HERE
        public async Task<TDto?> GetByAsync(object id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return entity == null
                ? null
                : _mapper.Map<TDto>(entity);
        }
    }
}