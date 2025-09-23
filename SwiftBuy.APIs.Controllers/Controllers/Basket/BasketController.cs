using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Controllers.Controllers.Base;
using SwiftBuy.Core.Application.Abstraction.Services.Basket;
using SwiftBuy.Shared.Models.Basket;

namespace SwiftBuy.APIs.Controllers.Controllers.Basket
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
        {
            var basket = await _basketService.GetCustomerBaskeAsync(id);
            if (basket is null)
                return NotFound();
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var updatedBasket = await _basketService.UpdateCustomerBasketAsync(customerBasketDto);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketById(string id)
        {
            await _basketService.DeleteCustomerBasketAsync(id);
        }
    }
}
