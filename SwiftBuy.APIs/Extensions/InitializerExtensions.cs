using SwiftBuy.Core.Domain.Contracts.Persistence;
using SwiftBuy.Infrastructure.Persistence._Data;

namespace SwiftBuy.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeSwiftBuyContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<SwiftBuyContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var swiftBuyContextInitializer = services.GetRequiredService<ISwiftBuyContextInitializer>();
            var swiftBuyIdentityContextInitializer = services.GetRequiredService<ISwiftBuyIdentityContextInitializer>();
            try
            {
                await swiftBuyContextInitializer.InitializeAsync();
                await swiftBuyContextInitializer.SeedAsync();

                await swiftBuyIdentityContextInitializer.InitializeAsync();
                await swiftBuyIdentityContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while applying migrations.");
            }
            return app;
        }
    }
}
