using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace old_stuff_exchange_v2.Entities
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options) {
            _configuration = configuration;
        }

        #region
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Building> Buildings { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            string connectionString = _configuration.GetConnectionString("LocalConnection");
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            /*foreach (IMutableProperty property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(10);
                property.SetScale(2);
            }

            foreach (IMutableProperty property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties()
                .Where(p => p.Name == "CreatedAt"))) {
                property.SetDefaultValueSql("getdate()");
            } */

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });


           /* modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasMany<Transaction>(w => w.Transactions).WithOne()
                    .HasForeignKey(w => w.WalletId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasMany<Transaction>(p => p.Transactions).WithOne()
                    .HasForeignKey(p => p.WalletId).OnDelete(DeleteBehavior.Restrict);
            });*/

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne<Post>(t => t.Post).WithMany()
                    .HasForeignKey(t => t.PostId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Wallet>(t => t.Wallet).WithMany()
                    .HasForeignKey(t => t.WalletId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.LastUpdatedAt).HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Remaining).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.Balance).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
                entity.Property(e => e.RequiredDeposit).HasColumnType("decimal(10,2)");
            });
        }
    }
}
