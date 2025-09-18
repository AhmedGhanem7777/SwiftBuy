using AutoMapper;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Abstraction.Services.Basket;
using SwiftBuy.Core.Application.Abstraction.Services.Order;
using SwiftBuy.Core.Application.Abstraction.Services.Product;
using SwiftBuy.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Lazy<IProductService> _productService;
        private Lazy<IBasketService> _basketService;
        private Lazy<IOrderService> _orderService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, Func<IBasketService> basketServiceFactory, Func<IOrderService> orderServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
            _orderService = new Lazy<IOrderService>(orderServiceFactory);
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
