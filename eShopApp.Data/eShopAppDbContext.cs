using eShopApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShopApp.Data
{
    public class eShopAppDbContext : DbContext
    {
        public eShopAppDbContext(DbContextOptions<eShopAppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.ProductId, x.OrderId });

            modelBuilder.Entity<Category>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<User>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Order>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Customer>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Product>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Role>().Property(x => x.IsActive).HasDefaultValue(true);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
