using Microsoft.Extensions.Logging;
using Store.Data;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDBContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    //Presist Data to db
                    
                    var brandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    if (brands != null)
                        await context.ProductBrands.AddRangeAsync(brands);
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    //Presist Data to db

                    var typeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    if (types != null)
                        await context.ProductTypes.AddRangeAsync(types);
                }
                if (context.Products != null && !context.Products.Any())
                {
                    //Presist Data to db

                    var prodData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(prodData);
                    if (products != null)
                        await context.Products.AddRangeAsync(products);
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    //Presist Data to db

                    var DelData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
                    var DELMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DelData);
                    if (DELMethods != null)
                        await context.DeliveryMethods.AddRangeAsync(DELMethods);
                }

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}
