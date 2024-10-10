using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Context
{
    public class StoreIdentityContext:IdentityDbContext<AppUser>
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options ):base(options)
        {
            
        }
    }
}
