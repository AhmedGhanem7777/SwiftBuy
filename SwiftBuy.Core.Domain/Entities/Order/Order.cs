using SwiftBuy.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Entities.Order
{
    public class Order : BaseAuditableEntity<int>
    {
        public required string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required Address ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public virtual DeliveryMethod? DeliveryMethod { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod!.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
