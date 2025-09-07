using SwiftBuy.Core.Application.Abstraction;
using System.Security.Claims;

namespace SwiftBuy.APIs.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? UserId { get; }
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.PrimarySid)?.Value;
        }
    }
}
