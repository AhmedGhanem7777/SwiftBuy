using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Controllers.Controllers.Base;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.APIs.Controllers.Controllers.Basket
{
    public class BasketController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
        {
            var basket = await _serviceManager.BasketService.GetCustomerBaskeAsync(id);
            if (basket is null)
                return NotFound();
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var updatedBasket = await _serviceManager.BasketService.UpdateCustomerBasketAsync(customerBasketDto);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketById(string id)
        {
            await _serviceManager.BasketService.DeleteCustomerBasketAsync(id);
        }
    }
}
