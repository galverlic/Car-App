using Car_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Avto> Cars { get; set; }


    }
}
