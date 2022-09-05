using System;
using StockPerformanceEvaluator.Database.Entities;

namespace StockPerformanceEvaluator.Services;

public interface IStockPriceUpdateService
{
    Task UpdateStockPricesAsync(string symbol, List<StockPriceEntity> pricesToUpdate);
}