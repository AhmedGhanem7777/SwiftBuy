using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using SwiftBuy.Core.Domain.Contracts.Infrastructure;
using SwiftBuy.Infrastructure.Basket_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection!);
            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            return services;
        }
    }
}
