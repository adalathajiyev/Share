using System;
using StockPerformanceEvaluator.Models;

namespace StockPerformanceEvaluator.Services.ApiClientService;

public interface IApiClient
{
   Task<Dictionary<string, DailyPrices>> getDailyStockPricesAsync(string symbol);
}