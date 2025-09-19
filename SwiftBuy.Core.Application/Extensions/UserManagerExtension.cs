using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftBuy.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Extensions
{
    public static class UserManagerExtension
    {
        public static Task<ApplicationUser?> FindUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = userManager.Users.Where(u => u.Email == email).Include(u => u.Address).FirstOrDefaultAsync();
            return user;
        }
    }
}
