using Microsoft.EntityFrameworkCore;

namespace BookShop.Data;
using Common;
using Microsoft.Extensions.Configuration;

public class BookShopDbContext : DbContext
{
    public BookShopDbContext()
    {
        
    }

    public BookShopDbContext(DbContextOptions options) :
        base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(optionsBuilder != null)
        {
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


}
