using SwiftBuy.Core.Application.Abstraction.Models.Auth;
using SwiftBuy.Core.Application.Abstraction.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> GettUserAddressAsync(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> UpdateUserAddressAsync(ClaimsPrincipal claimsPrincipal, AddressDto addressDto);
        Task<bool> CheckEmailExists(string email);
    }
}
