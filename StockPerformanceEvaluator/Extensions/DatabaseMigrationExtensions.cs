using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Npgsql;
using StockPerformanceEvaluator.Database;

namespace StockPerformanceEvaluator.Extensions;

public static class DatabaseMigrationExtensions
{
    public static async Task MigrateDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<StockPerformanceDBContext>();
        await context.Database.MigrateAsync();

        using (var conn = (NpgsqlConnection)context.Database.GetDbConnection())
        {
            conn.Open();
            conn.ReloadTypes();
        }
    }
}


