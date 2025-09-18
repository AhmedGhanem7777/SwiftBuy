using SwiftBuy.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Entities.Order
{
    public class OrderItem : BaseAuditableEntity<int>
    {
        public required ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
