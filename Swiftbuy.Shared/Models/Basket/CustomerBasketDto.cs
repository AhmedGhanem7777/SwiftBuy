using SwiftBuy.Shared.Models.Basket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Shared.Models.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
