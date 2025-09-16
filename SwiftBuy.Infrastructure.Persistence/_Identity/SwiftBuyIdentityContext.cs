using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwiftBuy.Core.Domain.Entities.Identity;
using SwiftBuy.Infrastructure.Persistence._Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Identity
{
    public class SwiftBuyIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public SwiftBuyIdentityContext(DbContextOptions<SwiftBuyIdentityContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly, type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(SwiftBuyIdentityContext));
        }
    }
}
