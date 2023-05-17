using Car_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }

        public DatabaseContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(a => a.Cars)
                .WithOne(o => o.Owner)
                .HasForeignKey(o => o.OwnerId);

            modelBuilder.Entity<Owner>()
                .HasMany(a => a.Cars)
                .WithOne(o => o.Owner)
                .HasForeignKey(o => o.OwnerId);

        }


    }
}
