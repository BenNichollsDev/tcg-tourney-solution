/*
Program: Local Games Store Management System
Filename: GenericService.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using TCG.Application.Interfaces;

namespace TCG.Application.Services
{
    public class GenericService<TEntity, TDto>
        : IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GenericService(IRepository<TEntity> repository, IMapper mapper, IUnitOfWork uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.UpdateAsync(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto?> DeleteAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.DeleteAsync(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto?> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            
            if (entity == null)
                return null;

            await _repository.DeleteAsync(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto?> GetByIdAsync(int id)
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

        public async Task<TDto?> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await _repository.GetByAsync(predicate);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }
    }
}
