using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Infrastructure.Data;

/// <summary>
/// Application database context with Identity support
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.HasOne(b => b.User)
                .WithOne(u => u.Basket)
                .HasForeignKey<Basket>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(b => b.UserId);
        });

        modelBuilder.Entity<BasketItem>(entity =>
        {
            entity.HasKey(bi => bi.Id);
            entity.HasOne(bi => bi.Basket)
                .WithMany(b => b.Items)
                .HasForeignKey(bi => bi.BasketId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(bi => bi.Price).HasPrecision(18, 2);
            entity.HasIndex(bi => bi.BasketId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(1000);
            entity.Property(p => p.Brand).HasMaxLength(100);
            entity.Property(p => p.Category).HasMaxLength(100);
            entity.Property(p => p.Price).HasPrecision(18, 2);
            entity.Property(p => p.DiscountPercentage).HasPrecision(5, 2);
            entity.HasIndex(p => p.Category);
        });

        modelBuilder.Entity<FavoriteProduct>(entity =>
        {
            entity.HasKey(fp => fp.Id);
            entity.HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(fp => fp.Product)
                .WithMany()
                .HasForeignKey(fp => fp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(fp => new { fp.UserId, fp.ProductId }).IsUnique();
        });
    }
}
