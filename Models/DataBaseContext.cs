using Microsoft.EntityFrameworkCore;

namespace Store_Db.Models;

public class DataBaseContext : DbContext
{
    public DbSet<Market> Market_table { get; set; }

    public DataBaseContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite($"Data Source=storeDb1.db");
    }
}