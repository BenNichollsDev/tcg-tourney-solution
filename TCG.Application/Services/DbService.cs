using System;
using System.Collections.Generic;
using System.Text;
using TCG.Application.Interfaces;

namespace TCG.Application.Services
{
    public class DbService
    {
        private readonly IRepository _repository;

        public DbService(IRepository repository)
        {
            _repository = repository;
        }

        //public async Task<List<T>> GetAllAsync<T>()
        //{

        //}
    }
}
