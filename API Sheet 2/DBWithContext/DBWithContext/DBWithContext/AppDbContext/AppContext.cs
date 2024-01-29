using DBWithContext.Models;
using System.Data.Entity;

namespace DBWithContext.AppDbContext
{
    public class AppContext : DbContext
    {
        public AppContext() : base("MySqlConnection")
        { }

        public DbSet<prdct01> Products { get; set; }
    }
}