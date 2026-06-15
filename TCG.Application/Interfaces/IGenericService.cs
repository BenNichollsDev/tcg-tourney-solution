//
// Program: Local Games Store Management System
// Filename: IGenericService.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 14/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TCG.Application.Interfaces
{
    public interface IGenericService<TEntity, TDto>
        where TDto : class
    {
        public Task<TDto> AddAsync(TDto dto);
        
        public Task<TDto> UpdateAsync(TDto dto);
        
        public Task<TDto?> DeleteAsync(TDto dto);
        
        public Task<TDto?> DeleteAsync(int id);

        public Task<TDto?> GetByIdAsync(int id);

        public Task<List<TDto>> GetAllAsync();

        public Task<TDto?> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}

