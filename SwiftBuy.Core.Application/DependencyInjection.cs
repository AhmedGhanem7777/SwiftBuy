using Microsoft.Extensions.DependencyInjection;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using SwiftBuy.Core.Application.Abstraction.Services.Basket;
using SwiftBuy.Core.Application.Abstraction.Services.Order;
using SwiftBuy.Core.Application.Mapping;
using SwiftBuy.Core.Application.Services;

namespace SwiftBuy.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            //services.AddScoped(typeof(Func<IBasketService>), (seviceProvider) =>
            //{
            //    return () => seviceProvider.GetRequiredService<IBasketService>();
            //});


            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(Func<IOrderService>), (seviceProvider) =>
            {
                return () => seviceProvider.GetRequiredService<IOrderService>();
            });

            return services;
        }
    }
}
