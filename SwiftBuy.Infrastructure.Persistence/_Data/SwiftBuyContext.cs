using Microsoft.EntityFrameworkCore;
using SwiftBuy.Core.Domain.Common.Entities;
using SwiftBuy.Core.Domain.Entities.Order;
using SwiftBuy.Core.Domain.Entities.Product;
using SwiftBuy.Infrastructure.Persistence._Common;
using SwiftBuy.Infrastructure.Persistence._Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Data
{
    public class SwiftBuyContext : DbContext
    {
        public SwiftBuyContext(DbContextOptions<SwiftBuyContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(SwiftBuyContext));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
