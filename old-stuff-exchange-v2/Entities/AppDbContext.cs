using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace old_stuff_exchange_v2.Entities
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        #region
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("DevConnection");
            if (connectionString == "Development")
            {
                connectionString = _configuration.GetConnectionString("DevConnection");
            }
            else {
                connectionString = _configuration.GetConnectionString("AzureConnection");
            }
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.LastUpdatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.Price).HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.Balance).HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.Balance).HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.Amount).HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.LastUpdatedAt).HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.CoinExchange).HasColumnType("decimal(10,0)");
            });
        }
    }
}
