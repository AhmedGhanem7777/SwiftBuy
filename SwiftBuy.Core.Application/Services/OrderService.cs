using AutoMapper;
using SwiftBuy.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using SwiftBuy.Core.Application.Abstraction.Models.Order;
using SwiftBuy.Core.Application.Abstraction.Services.Basket;
using SwiftBuy.Core.Application.Abstraction.Services.Order;
using SwiftBuy.Core.Application.Exceptions;
using SwiftBuy.Core.Domain.Common.Entities;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Entities.Order;
using SwiftBuy.Core.Domain.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto orderDto)
        {
            var basket = await _basketService.GetCustomerBaskeAsync(orderDto.BasketId);

            var orderItems = new List<OrderItem>();
            if (basket.Items.Count > 0)
            {
                var productRepo = _unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);

                    if (product is not null)
                    {
                        var productItemOrdered = new ProductItemOrdered()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl!
                        };

                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity,
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var address = _mapper.Map<Address>(orderDto.ShippingAddress);
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);

            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var orderSpecs = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId!);
            var existingOrder = await orderRepo.GetByIdWithSpecAsync(orderSpecs);
            if (existingOrder is not null)
            {
                orderRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }

            var order = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                SubTotal = subTotal,
                Items = orderItems,
                DeliveryMethod = deliveryMethod,
                PaymentIntentId = basket.PaymentIntentId!
            };
            await _unitOfWork.GetRepository<Order, int>().AddAsync(order);

            var created = await _unitOfWork.CompleteAsync() > 0;
            if (!created)
                throw new BadRequestException("An error has occured during creating the order");

            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpecs);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail, orderId);
            var order = await _unitOfWork.GetRepository<Order, int>().GetByIdWithSpecAsync(orderSpecs);
            if (order is null)
                throw new NotFoundException(nameof(Order), orderId);
            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }
    }
}
