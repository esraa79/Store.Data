using Store.Data.Entities;
using Store.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Store.Web.Helper;
using Store.Repository.Repositories;
using Store.Repository.Interfaces;
using AutoMapper;
using Store.Service.Services.ProductServices.Dtos;
using Store.Service.Services.ProductServices;
using Store.Web.Middleware;
using Microsoft.AspNetCore.Mvc;
using Store.Service.HandleResponses;
using Store.Web.Extentions;
using StackExchange.Redis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDBContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"),b=>b.MigrationsAssembly("Store.Data"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
            var configuration = (ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")));
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
