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
using Store.Data.Context;
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
            builder.Services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
            var configuration = (ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")));
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("  ", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http:localhost:4200", "https://localhost:7027/");
                });
            });
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
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
