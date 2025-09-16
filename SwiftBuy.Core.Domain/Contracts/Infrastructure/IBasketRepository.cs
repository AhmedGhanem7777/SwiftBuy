using SwiftBuy.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
