using Car_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Avto> Cars { get; set; }


    }
}
