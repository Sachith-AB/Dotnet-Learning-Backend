using Dotnet_backend.Migrations;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Stock entity
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stocks");

                entity.Property(s => s.Symbol)
                    .HasColumnType("varchar(10)")
                    .IsRequired();

                entity.Property(s => s.CompanyName)
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(s => s.Industry)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(s => s.Purchase)
                    .HasColumnType("decimal(18,2)");

                entity.Property(s => s.LastDiv)
                    .HasColumnType("decimal(18,2)");

                // Indexes
                entity.HasIndex(s => s.Symbol)
                    .IsUnique();
            });

            // Configure Comment entity
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.Property(c => c.Title)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                entity.Property(c => c.Content)
                    .HasColumnType("text")
                    .IsRequired();

                entity.Property(c => c.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relationship
                entity.HasOne(c => c.Stock)
                    .WithMany(s => s.Comments)
                    .HasForeignKey(c => c.StockId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new
            {
                p.AppUserId,
                p.StockId,
            }));

            modelBuilder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            modelBuilder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // For MySQL 5.7+ compatibility (if needed)
            modelBuilder.HasCharSet("utf8mb4");  // Full Unicode support
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // Global string configuration
            configurationBuilder.Properties<string>()
                .HaveMaxLength(255)  // Default max length
                .AreUnicode(false);  // Use varchar instead of nvarchar
        }
    }
}