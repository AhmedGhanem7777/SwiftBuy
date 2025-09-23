using Microsoft.EntityFrameworkCore;
using SwiftBuy.Core.Domain.Common;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Contracts.Persistence;
using SwiftBuy.Infrastructure.Persistence._Data;
using SwiftBuy.Infrastructure.Persistence.Repositories.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly SwiftBuyContext _dbContext;

        public GenericRepository(SwiftBuyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(bool withTracking = false)
            => withTracking ?
                   await _dbContext.Set<TEntity>().ToListAsync()
                   :
                   await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool withTracking = false)
            => withTracking ?
                   await ApplySpecifications(spec).ToListAsync()
                   :
                   await ApplySpecifications(spec).AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);
        
        public async Task<TEntity?> GetByIdWithSpecAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplySpecifications(spec).FirstOrDefaultAsync();

        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
            => SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplySpecifications(spec).CountAsync();
    }
}
