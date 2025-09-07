using Microsoft.Extensions.DependencyInjection;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Mapping;
using SwiftBuy.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
