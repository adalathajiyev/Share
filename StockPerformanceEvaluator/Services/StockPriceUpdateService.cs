using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockPerformanceEvaluator.Database;
using StockPerformanceEvaluator.Database.Entities;

namespace StockPerformanceEvaluator.Services;

public class StockPriceUpdateService : IStockPriceUpdateService
{
    private readonly StockPerformanceDBContext _dbContext;

    public StockPriceUpdateService(StockPerformanceDBContext dBContext)
    {
        _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
    }

    public async Task UpdateStockPricesAsync(string symbol, List<StockPriceEntity> pricesToUpdate)
    {
        var latestStockDate = await _dbContext.StockPrices.OrderBy(p => p.PriceDate)
            .FirstOrDefaultAsync(p => p.Symbol == symbol);

        var newPrices = pricesToUpdate.Where(p => latestStockDate == null || latestStockDate.PriceDate > latestStockDate.PriceDate);

        if(newPrices.Count() > 0)
        {
           await _dbContext.AddRangeAsync(newPrices);
           await _dbContext.SaveChangesAsync();
        }
    }

}

