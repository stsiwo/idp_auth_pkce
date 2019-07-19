using Microsoft.EntityFrameworkCore;
using OrderingApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure
{
    public class OrderingApiDbContext : DbContext
    {
        public OrderingApiDbContext(DbContextOptions<OrderingApiDbContext> options)
            : base(options)
        {
        }

        public OrderingApiDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User ===1:0...1=== Cart
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId) 
                .IsRequired(false);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
                

            // User ===1:0...N=== Order
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(c => c.User)
                .IsRequired(false);
                

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(c => c.UserId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            // Cart ===1:0...N=== Product
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Cart)
                .IsRequired(false);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Cart)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CartId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            // Order ===0...1:1...N=== Product
            modelBuilder.Entity<Order>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Order)
                .IsRequired(true);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Order)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.OrderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Value Objects (OwnedEntity): create columns for each property of Address Value Object inside User table
            modelBuilder.Entity<User>().OwnsOne(u => u.Address);


            // define db index 
            // #REFACTOR
        }
    }

}
