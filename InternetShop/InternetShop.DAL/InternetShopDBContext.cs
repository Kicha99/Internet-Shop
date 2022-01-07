using InternetShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL
{
    class InternetShopDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasMany(x => x.Products);
            modelBuilder.Entity<Category>()
                .HasMany(x => x.Child);
            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .HasMany(x => x.Products)
                .WithMany(x => x.Orders);
            modelBuilder.Entity<Product>()
                .ToTable("Products");

        }
    }
}
