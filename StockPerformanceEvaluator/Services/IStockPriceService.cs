using System;
using StockPerformanceEvaluator.Models;

namespace StockPerformanceEvaluator.Services;

public interface IStockPriceService
{
    Task<StockPerformanceDTO> EvaluateDailyStockPerformance(string symbol);

    Task<List<decimal>> EvaluateIntradayStockPerformance(string symbol);
}