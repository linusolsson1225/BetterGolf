using BetterGolfASP.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BetterGolfASP.DB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<IronClub> IronClubs { get; set; }
        public DbSet<WoodClub> WoodClubs { get; set; }
        public DbSet<PutterClub> PutterClubs { get; set; }
        public DbSet<GolfClub> GolfClubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<GolfClub>()
                .HasDiscriminator<string>("ClubType")
                .HasValue<IronClub>("Iron")
                .HasValue<WoodClub>("Wood")
                .HasValue<PutterClub>("Putter");
        }

        public Context() { }
    }
}