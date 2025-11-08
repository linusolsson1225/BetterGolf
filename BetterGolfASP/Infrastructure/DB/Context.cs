using BetterGolfASP.Domain.Models;
using BetterGolfASP.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BetterGolfASP.Infrastructure.DB;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public Context() { }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductVariant> ProductVariants { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderRow> OrderRows { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Discriminator för Product-arv
        modelBuilder.Entity<Product>()
            .HasDiscriminator<string>("ProductType")
            .HasValue<Product>("Product")
            .HasValue<GolfClub>("GolfClub")
            .HasValue<IronClub>("IronClub")
            .HasValue<WoodClub>("WoodClub")
            .HasValue<PutterClub>("PutterClub")
            .HasValue<GolfBall>("GolfBall")
            .HasValue<Clothing>("Clothing");

        // ImgUrls conversion
        modelBuilder.Entity<Product>()
            .Property(p => p.ImgUrls)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .Metadata.SetValueComparer(
                new ValueComparer<List<string>>(
                    (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );

        // Precision för pris
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderRow>()
            .Property(r => r.Price)
            .HasPrecision(18, 2);

        // Product → Variants 
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Order → OrderRows
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderRows)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // OrderRow → Product
        modelBuilder.Entity<OrderRow>()
            .HasOne(r => r.Product)
            .WithMany()
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // **OrderRow → ProductVariant 
        modelBuilder.Entity<OrderRow>()
            .HasOne(r => r.Variant)
            .WithMany()
            .HasForeignKey(r => r.VariantId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.Restrict);

        // Order → Customer
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
