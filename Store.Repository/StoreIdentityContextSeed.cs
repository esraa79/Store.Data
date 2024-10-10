using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Esraa Atef",
                    Email = "esraa57.ea@gmail.com",
                    UserName = "esraaAtef",
                    Address = new Address
                    {
                        FirstName = "Esraa",
                        LastName = "Atef",                        
                        City = "Jeddah",
                        Street = "Watan",
                        PostalCode = "1234"                     
                        

                    }
                };
                await userManager.CreateAsync(user, "PSW123!");
            }
            }

    }
}
