/*
Program: Local Games Store Management System
Filename: Repository.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
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

        public async Task<TEntity?> GetByIdAsync(int id)
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

        // These are synchronous EF operations.
        // They only mark the entity state as Modified/Deleted.
        public Task UpdateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
