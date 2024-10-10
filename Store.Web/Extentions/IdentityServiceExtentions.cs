using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Store.Data.Context;
using Store.Data.Entities.IdentityEntities;

namespace Store.Web.Extentions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser> ();
            builder = new IdentityBuilder(builder.UserType,builder.Services);
            builder.AddEntityFrameworkStores<StoreIdentityContext>();
            
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication();
            return services;

        }
    }
}
