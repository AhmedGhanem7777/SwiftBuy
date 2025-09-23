using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Data.Interceptors
{
    internal class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public AuditInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null)
                return;

            var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
                                                 .Where(entity => entity.State is EntityState.Added or EntityState.Modified);
            foreach (var entry in entries)
            {
                if (string.IsNullOrEmpty(_loggedInUserService.UserId))
                    _loggedInUserService.UserId = "User";
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = _loggedInUserService.UserId!;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
