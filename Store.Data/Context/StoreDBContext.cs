﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data
{
    public class StoreDBContext:DbContext
    {
        
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
        {
            
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }



    }
}
