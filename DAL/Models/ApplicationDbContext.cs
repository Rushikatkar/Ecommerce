using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets for Entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding the Category Table
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics", Description = "Devices, gadgets, and accessories." },
                new Category { CategoryId = 2, Name = "Fashion", Description = "Clothing, footwear, and accessories." },
                new Category { CategoryId = 3, Name = "Home & Kitchen", Description = "Appliances, decor, and kitchen tools." },
                new Category { CategoryId = 4, Name = "Books", Description = "Fiction, non-fiction, and educational materials." },
                new Category { CategoryId = 5, Name = "Sports", Description = "Sporting goods and outdoor equipment." }
            );
        }
    }
}
