using SwiftBuy.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Entities.Order
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public required string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
