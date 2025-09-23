using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using SwiftBuy.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using SwiftBuy.Infrastructure.Basket_Repository;
using SwiftBuy.Infrastructure.Cache_Service;
using SwiftBuy.Infrastructure.Payment_Service;

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
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            //services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));
            return services;
        }
    }
}
