using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftBuy.Core.Domain.Entities.Order;
using SwiftBuy.Infrastructure.Persistence._Data.Configs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Data.Configs.Orders
{
    internal class DeliveryMethodConfigurations : BaseEntityConfigurations<DeliveryMethod, int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);

            builder.Property(deliveryMethod => deliveryMethod.Cost)
                .HasColumnType("decimal(8,2)");
        }
    }
}
