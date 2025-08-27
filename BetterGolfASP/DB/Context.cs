using BetterGolfASP.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BetterGolfASP.DB
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<IronClub> IronClubs { get; set; }
        public DbSet<WoodClub> WoodClubs { get; set; }
        public DbSet<PutterClub> PutterClubs { get; set; }
        public Context() { }
    }
}
