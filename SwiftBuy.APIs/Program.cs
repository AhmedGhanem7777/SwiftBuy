using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Extensions;
using SwiftBuy.APIs.Services;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application;
using SwiftBuy.Infrastructure.Persistence;
using SwiftBuy.Infrastructure;
using SwiftBuy.APIs.Controllers.Errors;
using SwiftBuy.APIs.Middlewares;
using SwiftBuy.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using SwiftBuy.Infrastructure.Persistence._Identity;
namespace SwiftBuy.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                                         .Select(P => new ApiValidationErrorResponse.ValidationError()
                                                         {
                                                             Field = P.Key,
                                                             Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
                                                         });

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            builder.Services.AddIdentityServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            #region Apply all pending migrations [Update-Database] and Data Seeding
            await app.InitializeSwiftBuyContextAsync();
            #endregion


            #region Configure Kestrel Middleware

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
