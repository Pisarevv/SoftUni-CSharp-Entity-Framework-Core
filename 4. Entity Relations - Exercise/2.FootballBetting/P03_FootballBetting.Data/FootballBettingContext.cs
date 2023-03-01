namespace P03_FootballBetting.Data;

using Microsoft.EntityFrameworkCore;

using Common;
using Models;

public class FootballBettingContext : DbContext
{
    protected FootballBettingContext()
    {

    }

    protected FootballBettingContext(DbContextOptions options)
        :base (options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayerStatistic>(entity =>
        {
            entity.HasKey(ps => new { ps.PlayerId, ps.GameId });
        });
    }
}
