using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Infrastructure.Persistence._Data;
using SwiftBuy.Infrastructure.Persistence._Data.Interceptors;
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
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(ISwiftBuyContextInitializer), typeof(SwiftBuyContextInitializer));

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterceptor));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            return services;
        }
    }
}
