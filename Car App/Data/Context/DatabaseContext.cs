using Car_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Avto> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(a => a.Cars)
                .WithOne(o => o.Owner)
                .HasForeignKey(o => o.OwnerId);
        }


    }
}
