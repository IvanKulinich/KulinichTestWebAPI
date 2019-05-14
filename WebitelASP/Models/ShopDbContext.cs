using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebitelASP.Models
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public ShopDbContext() : base("name=DbConnect")
        {
            Database.SetInitializer(new ShopInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(product => product.Price).HasPrecision(18, 0);       
            modelBuilder.Entity<Order>().Property(order => order.Amount).HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
               .HasMany(product => product.OrderProducts)
               .WithOptional(orderProduct => orderProduct.Product)
               .WillCascadeOnDelete();
            modelBuilder.Entity<Order>()
                .HasMany(order => order.OrderProducts)
                .WithOptional(orderProduct => orderProduct.Order)
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }
    }
}