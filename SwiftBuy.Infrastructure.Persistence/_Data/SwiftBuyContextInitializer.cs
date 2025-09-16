using Microsoft.EntityFrameworkCore;
using SwiftBuy.Core.Domain.Common.Entities;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Data
{
    internal class SwiftBuyContextInitializer : ISwiftBuyContextInitializer
    {
        private readonly SwiftBuyContext _dbContext;

        public SwiftBuyContextInitializer(SwiftBuyContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (_dbContext.Brands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../SwiftBuy.Infrastructure.Persistence/_Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands is not null && brands.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Categories.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../SwiftBuy.Infrastructure.Persistence/_Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories is not null && categories.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Products.Count() == 0)
            {
                var productssData = File.ReadAllText("../SwiftBuy.Infrastructure.Persistence/_Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productssData);
                if (products is not null && products.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
