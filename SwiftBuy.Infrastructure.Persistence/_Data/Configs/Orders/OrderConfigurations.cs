using SwiftBuy.Infrastructure.Persistence._Data.Configs.Base;
using SwiftBuy.Core.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;

namespace SwiftBuy.Infrastructure.Persistence._Data.Configs.Orders
{
    internal class OrderConfigurations : BaseAuditableEntityConfigurations<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(order => order.Status)
                .HasConversion(
                       (OStatus => OStatus.ToString()),
                       (OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus))
                );

            builder.OwnsOne(order => order.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.HasOne(order => order.DeliveryMethod)
                .WithMany()
                .HasForeignKey(order => order.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(order => order.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(order => order.SubTotal)
                .HasColumnType("decimal(8,2)");
        }
    }
}
