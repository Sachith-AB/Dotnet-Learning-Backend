using Dotnet_backend.Models;
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

        public DbSet<Stock> Stocks { get; set; }  // Changed to plural for convention
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Stock entity
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stocks");  // Explicit table name

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