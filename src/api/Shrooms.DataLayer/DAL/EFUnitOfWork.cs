﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Infrastructure;

namespace Shrooms.DataLayer.DAL
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories;
        //private readonly IApplicationSettings _appSettings;

        public IDbContext DbContext { get; }

        public EfUnitOfWork(IDbContext context)
        {
            DbContext = context;
            //_appSettings = appSettings;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>(int organizationId = 2)
            where TEntity : class
        {
            throw new NotImplementedException();
            //IRepository<TEntity> repository;

            //if (_repositories.Keys.Contains(typeof(TEntity)))
            //{
            //    repository = _repositories[typeof(TEntity)] as IRepository<TEntity>;
            //    return repository;
            //}

            ////repository = new EfRepository<TEntity>(DbContext, _appSettings);
            ////_repositories.Add(typeof(TEntity), repository);
            //return repository;
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public T GetDbContextAs<T>()
            where T : class, IDbContext
        {
            return DbContext as T;
        }
    }
}