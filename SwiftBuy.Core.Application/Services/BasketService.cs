using AutoMapper;
using SwiftBuy.Core.Application.Abstraction.Models;
using SwiftBuy.Core.Application.Abstraction.Services.Basket;
using SwiftBuy.Core.Domain.Contracts.Infrastructure;
using SwiftBuy.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> GetCustomerBaskeAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return _mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(customerBasketDto);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (updatedBasket == null)
                throw new Exception("Problem updating the basket");
            return customerBasketDto;
        }
     
        public async Task DeleteCustomerBasketAsync(string basketId)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(basketId);
            if (!deleted)
                throw new Exception("Problem deleting the basket");
        }
    }
}
