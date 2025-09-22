using SwiftBuy.Core.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecification : BaseSpecifications<Order, int>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId)
           :base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
