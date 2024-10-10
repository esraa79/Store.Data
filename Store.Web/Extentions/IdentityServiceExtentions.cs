using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Context;
using Store.Data.Entities.IdentityEntities;
using System.Text;

namespace Store.Web.Extentions
{
    public static class IdentityServiceExtentions
    {
      
       
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _config)
        {
            var builder = services.AddIdentity<AppUser,IdentityRole> ();
            builder = new IdentityBuilder(builder.UserType,typeof(IdentityRole),builder.Services);
            builder.AddEntityFrameworkStores<StoreIdentityContext>();
            
            builder.AddSignInManager<SignInManager<AppUser>>();
            builder.AddUserManager<UserManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _config["Token:Issuer"],
                        ValidateAudience=false,
                    };
                });
            return services;

        }
    }
}
