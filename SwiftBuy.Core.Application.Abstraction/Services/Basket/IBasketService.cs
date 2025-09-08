using SwiftBuy.Core.Application.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBaskeAsync(string basketId);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto);
        Task DeleteCustomerBasketAsync(string basketId);
    }
}
