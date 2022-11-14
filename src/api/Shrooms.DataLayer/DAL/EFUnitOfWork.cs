using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.Options;

namespace Shrooms.DataLayer.DAL
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories;
        private readonly ApplicationOptions _applicationOptions;

        public IDbContext DbContext { get; }

        public EfUnitOfWork(IDbContext context, IOptions<ApplicationOptions> applicationOptions)
        {
            DbContext = context;
            _applicationOptions = applicationOptions.Value;

            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>(int organizationId = 2)
            where TEntity : class
        {
            IRepository<TEntity> repository;

            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                repository = _repositories[typeof(TEntity)] as IRepository<TEntity>;
                return repository;
            }

            repository = new EfRepository<TEntity>(DbContext);

            _repositories.Add(typeof(TEntity), repository);

            return repository;
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