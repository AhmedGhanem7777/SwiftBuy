using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SwiftBuy.Core.Application.Abstraction.Models.Auth;
using SwiftBuy.Core.Application.Abstraction.Services.Auth;
using SwiftBuy.Core.Application.Services;
using SwiftBuy.Core.Domain.Entities.Identity;
using SwiftBuy.Infrastructure.Persistence._Identity;
using System.Text;

namespace SwiftBuy.APIs.Extensions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SwiftBuyIdentityContext>();

            services.Configure<IdentityOptions>((identityOptions) =>
            {
                // Users Props
                identityOptions.User.RequireUniqueEmail = true;

                // SignIn Props
                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                // Password Props
                // identityOptions.Password.RequireNonAlphanumeric = true;
                // identityOptions.Password.RequiredUniqueChars = 2;
                // identityOptions.Password.RequiredLength = 6;
                // identityOptions.Password.RequireLowercase = true;
                // identityOptions.Password.RequireUppercase = true;
                // identityOptions.Password.RequireDigit = true;

                // Lockout Props
                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.Configure<JwtSettings>(configuration.GetSection("JWT"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? string.Empty)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
