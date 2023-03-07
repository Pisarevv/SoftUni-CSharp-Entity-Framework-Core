namespace BookShop.Data;
using Microsoft.EntityFrameworkCore;
using Common;
using BookShop.Models;

public class BookShopContext : DbContext
{
    public BookShopContext()
    {
        
    }

    public BookShopContext(DbContextOptions options) :
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
        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(bc => new { bc.CategoryId, bc.BookId });
        });

        base.OnModelCreating(modelBuilder);
    }


}
