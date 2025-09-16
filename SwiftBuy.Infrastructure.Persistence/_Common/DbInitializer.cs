using Microsoft.EntityFrameworkCore;
using SwiftBuy.Core.Domain.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Common
{
    public abstract class DbInitializer : IDbInitializer
    {
        private readonly DbContext _dbContext;

        protected DbInitializer(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
