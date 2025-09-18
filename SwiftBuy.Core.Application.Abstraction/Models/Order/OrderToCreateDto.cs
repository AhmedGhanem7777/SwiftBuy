using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Models.Order
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDto ShippingAddress { get; set; }
    }
}
