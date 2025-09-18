using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftBuy.Core.Domain.Entities.Identity;
using SwiftBuy.Infrastructure.Persistence._Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Identity.Config
{
    [DbContextTypeAttribute(typeof(SwiftBuyIdentityContext))]
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(nameof(Address.Id))
                .ValueGeneratedOnAdd();

            builder.Property(A => A.FirstName)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(A => A.LastName)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(A => A.Street)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(A => A.City)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(A => A.Country)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.ToTable("Addresses");
        }
    }
}
