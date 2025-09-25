using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftBuy.AdminDashboard.Helper;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Entities.Identity;
using SwiftBuy.Infrastructure.Persistence._Data;
using SwiftBuy.Infrastructure.Persistence._Identity;
using SwiftBuy.Infrastructure.Persistence.UnitOfWork;

namespace SwiftBuy.AdminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SwiftBuyContext>((serviceProvider, options) =>
            {
                options
                //.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                /*.AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>())*/;
            });

            builder.Services.AddDbContext<SwiftBuyIdentityContext>(options =>
            {
                options/*.UseLazyLoadingProxies()*/.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext"));
            });

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SwiftBuyIdentityContext>();

            builder.Services.Configure<IdentityOptions>((identityOptions) =>
            {
                //// Users Props
                //identityOptions.User.RequireUniqueEmail = true;

                //// SignIn Props
                //identityOptions.SignIn.RequireConfirmedAccount = true;
                //identityOptions.SignIn.RequireConfirmedEmail = true;
                //identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                // Password Props
                identityOptions.Password.RequireNonAlphanumeric = true;
                //identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequireDigit = true;

                //// Lockout Props
                //identityOptions.Lockout.AllowedForNewUsers = true;
                //identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            // Auto Mapper Configurations
            builder.Services.AddAutoMapper(typeof(Mapping));

            // Unit of Work
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}
