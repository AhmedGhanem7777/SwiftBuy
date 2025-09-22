using SwiftBuy.Shared.Models.Basket;

namespace SwiftBuy.Core.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBaskeAsync(string basketId);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto);
        Task DeleteCustomerBasketAsync(string basketId);
    }
}
