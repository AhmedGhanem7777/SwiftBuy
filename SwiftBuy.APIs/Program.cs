
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftBuy.APIs.Extensions;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Infrastructure.Persistence;
using SwiftBuy.Infrastructure.Persistence._Data;

namespace SwiftBuy.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddPersistenceServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            #region Apply all pending migrations [Update-Database] and Data Seeding
            await app.InitializeSwiftBuyContextAsync();
            #endregion

            #region Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
