using BetterGolfASP.Domain.Models;
using BetterGolfASP.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace BetterGolfASP.Infrastructure.DB
{
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

            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<Product>("Product")
                .HasValue<GolfClub>("GolfClub")
                .HasValue<IronClub>("IronClub")
                .HasValue<WoodClub>("WoodClub")
                .HasValue<PutterClub>("PutterClub")
                .HasValue<GolfBall>("GolfBall")
                .HasValue<Clothing>("Clothing");

            modelBuilder.Entity<Product>()
                .Property(p => p.ImgUrls)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(v => v.Product) 
                .HasForeignKey(v => v.ProductID) 
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderRows)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderRow>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);
        }
    }
}