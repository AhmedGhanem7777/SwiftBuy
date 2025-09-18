using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Models.Order
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public required string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
