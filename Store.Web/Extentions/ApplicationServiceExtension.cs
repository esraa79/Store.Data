using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.ProductServices.Dtos;
using Store.Service.Services.ProductServices;
using Store.Service.HandleResponses;
using Store.Service.CachService;
using Store.Service.BasketService.Dtos;
using Store.Service.BasketService;
using Store.Repository.Basket;

namespace Store.Web.Extentions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICachService, CachService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState.Where(model => model.Value?.Errors.Count > 0)
                                                       .SelectMany(model => model.Value?.Errors)
                                                       .Select(error => error.ErrorMessage).ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
