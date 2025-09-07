using SwiftBuy.Core.Domain.Common;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Infrastructure.Persistence._Data;
using SwiftBuy.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SwiftBuyContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(SwiftBuyContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ConcurrentDictionary<string, object>();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, () => new GenericRepository<TEntity, TKey>(_dbContext));
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}
