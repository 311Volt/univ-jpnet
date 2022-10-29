using JPNetEF.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.App
{
    internal class AppDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=.\\store.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TPH
            modelBuilder.Entity<Client>()
                .HasDiscriminator<int>("ClientType")
                .HasValue<Client>(0)
                .HasValue<ECommClient>(1);

            //TPT
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<ECommOrder>().ToTable("ECommOrders");

            base.OnModelCreating(modelBuilder);
        }
    }
}
