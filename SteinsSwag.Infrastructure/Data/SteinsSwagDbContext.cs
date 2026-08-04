using Microsoft.EntityFrameworkCore;
using SteinsSwag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Infrastructure.Data
{
    public class SteinsSwagDbContext : DbContext
    {
        public SteinsSwagDbContext(DbContextOptions<SteinsSwagDbContext> options)
        : base(options) { }

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Seller> Sellers => Set<Seller>();
        public DbSet<PlacementSlot> PlacementSlots => Set<PlacementSlot>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(i => i.Name).IsRequired().HasMaxLength(200);
                entity.Property(i => i.Price).HasColumnType("decimal(10,2)");

                entity.HasOne(i => i.Category)
                    .WithMany(c => c.Items)
                    .HasForeignKey(i => i.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Seller)
                    .WithMany(s => s.Items)
                    .HasForeignKey(i => i.SellerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(s => s.Name).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<PlacementSlot>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("decimal(10,2)");

                entity.HasOne(p => p.Seller)
                    .WithMany(s => s.PlacementSlots)
                    .HasForeignKey(p => p.SellerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");

                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(10,2)");

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Item)
                    .WithMany(i => i.OrderItems)
                    .HasForeignKey(oi => oi.ItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
