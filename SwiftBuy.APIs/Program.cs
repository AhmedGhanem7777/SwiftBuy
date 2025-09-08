using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftBuy.APIs.Extensions;
using SwiftBuy.APIs.Services;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Infrastructure.Persistence;
using SwiftBuy.Infrastructure.Persistence._Data;
using SwiftBuy.APIs.Controllers;
namespace SwiftBuy.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(AssemblyInformation).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            builder.Services.AddApplicationServices();
            #endregion

            var app = builder.Build();

            #region Apply all pending migrations [Update-Database] and Data Seeding
            await app.InitializeSwiftBuyContextAsync();
            #endregion


            #region Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
