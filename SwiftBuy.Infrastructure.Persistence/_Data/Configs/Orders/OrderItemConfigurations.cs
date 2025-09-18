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
    internal class OrderItemConfigurations : BaseAuditableEntityConfigurations<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(orderItem => orderItem.Product, product => product.WithOwner());

            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(8,2)");
        }
    }
}
