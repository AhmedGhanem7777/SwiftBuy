using Microsoft.AspNetCore.Identity;
using SwiftBuy.Core.Domain.Contracts.Persistence;
using SwiftBuy.Core.Domain.Entities.Identity;
using SwiftBuy.Infrastructure.Persistence._Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Identity
{
    public class SwiftBuyIdentityContextInitializer : DbInitializer, ISwiftBuyIdentityContextInitializer
    {
        private readonly SwiftBuyIdentityContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SwiftBuyIdentityContextInitializer(SwiftBuyIdentityContext dbContext, UserManager<ApplicationUser> userManager)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public override async Task SeedAsync()
        {
            if (!_dbContext.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Ahmed Ghanem",
                    UserName = "ahmed.ghanem",
                    Email = "ahmed.ghanem@gmail.com",
                    PhoneNumber = "01055888458"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
