using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Models.Order
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public required AddressDto ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public virtual string? DeliveryMethod { get; set; }
        public virtual ICollection<OrderItemDto> Items { get; set; }
        public decimal SubTotal { get; set; }
         public string? PaymentIntentId { get; set; }
    }
}
