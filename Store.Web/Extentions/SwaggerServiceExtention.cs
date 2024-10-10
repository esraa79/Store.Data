using Microsoft.OpenApi.Models;

namespace Store.Web.Extentions
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { 
                        Title = "Store api", 
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "John Walkner",
                            Email = "John.Walkner@gmail.com",
                            Url = new Uri("https://twitter.com/jwalkner"),
                        },
                    });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "API key needed to access the endpoints",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                option.AddSecurityDefinition("bearer", securityScheme);
                var securityRequirments = new OpenApiSecurityRequirement
                {
                    { securityScheme,new[] {"bearer"} }
                };
                option.AddSecurityRequirement(securityRequirments);
               });
            return services;
        }
    }
}
