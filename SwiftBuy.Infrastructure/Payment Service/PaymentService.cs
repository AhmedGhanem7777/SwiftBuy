using Stripe;
using Product = SwiftBuy.Core.Domain.Common.Entities.Product;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Entities.Order;
using SwiftBuy.Shared.Exceptions;
using SwiftBuy.Shared.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using SwiftBuy.Shared.Models;
using Microsoft.Extensions.Configuration;
using Stripe.V2;
using Microsoft.Extensions.Logging;
using SwiftBuy.Core.Domain.Specifications.Orders;
using SwiftBuy.Core.Application.Abstraction.Common.Contracts.Infrastructure;

namespace SwiftBuy.Infrastructure.Payment_Service
{
    public class PaymentService(IOptions<StripeSettings> stripeSettings, ILogger<PaymentService> _logger, IConfiguration _configuration, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentService
    {
        private readonly StripeSettings _stripeSettings = stripeSettings.Value;

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) throw new NotFoundException(nameof(CustomerBasketDto), basketId);

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod is null)
                    throw new NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = _unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (product is null)
                        throw new NotFoundException(nameof(Product), item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            PaymentIntent? paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) // Create New PaymentIntent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(basket.ShippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update PaymentIntent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(basket.ShippingPrice * 100),
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);
            return _mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatus(string requestBody, string header)
        {
            var stripeEvent = EventUtility.ConstructEvent(requestBody, header, _stripeSettings.WebhookSecret);

            // Handle the event
            var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;
            Order? order;
            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    order = await UpdateOrder(paymentIntent.Id, true);
                    _logger.LogInformation("Payment Succeeded: {id}", paymentIntent.Id);
                    break;
                case "payment_intent.payment_failed":
                    order = await UpdateOrder(paymentIntent.Id, false);
                    _logger.LogInformation("Payment Failed: {id}", paymentIntent.Id);
                    break;
            }
        }

        private async Task<Order?> UpdateOrder(string paymentIntentId, bool isPaid)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
            var order = await orderRepo.GetByIdWithSpecAsync(spec);
            if (order is null) throw new NotFoundException(typeof(Order).Name, $"PaymentIntentId: {paymentIntentId}");
            if (isPaid)
                order.Status = OrderStatus.PaymentReceived;
            else
                order.Status = OrderStatus.PaymentFailed;
            orderRepo.Update(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }
    }
}
