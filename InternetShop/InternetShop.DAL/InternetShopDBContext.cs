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
        public InternetShopDBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-InternetShop.UI-E6895286-D0FA-4B5F-BA11-FCDDF259D9AC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //string connectionString = @"Data Source=TEL-AVIV;Initial Catalog=sandbox;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasMany(x => x.Products)
                .WithOne(x => x.Category);
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
