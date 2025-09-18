using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Controllers.Controllers.Base;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Abstraction.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.APIs.Controllers.Controllers.Order
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public OrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderToCreateDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderToCreateDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!, id);
            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
