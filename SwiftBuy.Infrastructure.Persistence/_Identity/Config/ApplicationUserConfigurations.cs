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
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.DisplayName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(U => U.Address)
                .WithOne(A => A.User)
                .HasForeignKey<Address>(A => A.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
