using StackExchange.Redis;
using SwiftBuy.Core.Domain.Contracts.Infrastructure;
using SwiftBuy.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Basket_Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        async Task<CustomerBasket?> IBasketRepository.GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        async Task<CustomerBasket?> IBasketRepository.UpdateBasketAsync(CustomerBasket basket)
        {
            var createdOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(20));
            return createdOrUpdated ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
    }
}
