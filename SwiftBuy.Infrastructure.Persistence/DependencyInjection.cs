using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Contracts.Persistence;
using SwiftBuy.Infrastructure.Persistence._Data;
using SwiftBuy.Infrastructure.Persistence._Data.Interceptors;
using SwiftBuy.Infrastructure.Persistence._Identity;
using SwiftBuy.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SwiftBuyContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<SwiftBuyIdentityContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            });

            services.AddScoped(typeof(ISwiftBuyContextInitializer), typeof(SwiftBuyContextInitializer));
            services.AddScoped(typeof(ISwiftBuyIdentityContextInitializer), typeof(SwiftBuyIdentityContextInitializer));

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterceptor));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            return services;
        }
    }
}
