using System;
using Microsoft.EntityFrameworkCore;
using StockPerformanceEvaluator.Database.Entities;

namespace StockPerformanceEvaluator.Database;

public class StockPerformanceDBContext : DbContext
{
    public DbSet<StockPriceEntity> StockPrices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<StockPriceEntity>()
                   .Property(e => e.Id)
                   .UseHiLo("stock_price_sequence");
    }

    public StockPerformanceDBContext(DbContextOptions<StockPerformanceDBContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateDates();

        return base.SaveChanges();
    }

    private void UpdateDates()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
