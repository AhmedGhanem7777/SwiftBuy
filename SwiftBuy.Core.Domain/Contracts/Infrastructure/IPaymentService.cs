using SwiftBuy.Core.Domain.Entities.Basket;
using SwiftBuy.Core.Domain.Entities.Order;
using SwiftBuy.Shared.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId);
        Task UpdateOrderPaymentStatus(string requestBody, string header);
    }
}
