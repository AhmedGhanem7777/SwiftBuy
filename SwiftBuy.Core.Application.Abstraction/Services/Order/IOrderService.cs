using SwiftBuy.Core.Application.Abstraction.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Services.Order
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto orderDto);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);
        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
