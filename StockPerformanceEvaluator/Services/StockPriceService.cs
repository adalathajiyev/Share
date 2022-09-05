using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StockPerformanceEvaluator.Controllers;
using StockPerformanceEvaluator.Database.Entities;
using StockPerformanceEvaluator.Models;
using StockPerformanceEvaluator.Services.ApiClientService;

namespace StockPerformanceEvaluator.Services;

public class StockPriceService : IStockPriceService
{
    private readonly IApiClient _apiClient;
    private readonly IConfiguration _configuration;
    private readonly IStockPriceUpdateService _updateService;

    public StockPriceService(IApiClient apiClient, IConfiguration configuration, IStockPriceUpdateService updateService)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
    }

    public async Task<StockPerformanceDTO> EvaluateDailyStockPerformance(string symbol)
    {
        var benchmarkStockName = _configuration["BenchmarkStockName"];

        var inputstockDailyPrices = await GetDailyPrices(symbol);

        var benchmarkStockDailyPrices = await GetDailyPrices(benchmarkStockName);

        await _updateService.UpdateStockPricesAsync(symbol ,inputstockDailyPrices);

        await _updateService.UpdateStockPricesAsync(benchmarkStockName, benchmarkStockDailyPrices);

        var results = CalculatePerformance(inputstockDailyPrices, benchmarkStockDailyPrices);

        results.InputSymbol = symbol;
        results.BenchmarkSymbol = benchmarkStockName;

        return results;
    }

    public Task<List<decimal>> EvaluateIntradayStockPerformance(string symbol)
    {
        throw new NotImplementedException();
    }

    private async Task<List<StockPriceEntity>> GetDailyPrices(string symbol)
    {
        var dailyPricesDictionary = await _apiClient.getDailyStockPricesAsync(symbol);

        return MapStockPriceEntities(dailyPricesDictionary, symbol);
    }

    private List<StockPriceEntity> MapStockPriceEntities(Dictionary<string, DailyPrices> dailyPrices, string symbol)
    {
        var stockPriceEntities = new List<StockPriceEntity>();

        foreach (var price in dailyPrices.Take(7).Reverse())
        {
            var stockPrice = new StockPriceEntity
            {
                PriceDate = DateTime.Parse(price.Key).ToUniversalTime(),
                Symbol = symbol,
                High = price.Value.High,
                Close = price.Value.Close,
                Low = price.Value.Low,
                Volume = price.Value.Volume,
            };

            stockPriceEntities.Add(stockPrice);
        }

        return stockPriceEntities;
    }

    private StockPerformanceDTO CalculatePerformance(List<StockPriceEntity> inputStockPrices, List<StockPriceEntity> benchmarkStockPrices)
    {
        var stockPerformanceDTO = new StockPerformanceDTO
        {
            InputSymbol = "",
            BenchmarkSymbol = "",
            StockPerformance = new List<DailyPerformanceDTO>(),
        };

        for (int i = 0; i < inputStockPrices.Count(); i++)
        {
            stockPerformanceDTO.StockPerformance.Add(new DailyPerformanceDTO
            {
                PriceDate = inputStockPrices[i].PriceDate.ToShortDateString(),
                InputPerformance = Math.Round((inputStockPrices[i].Close - inputStockPrices[0].Close) / inputStockPrices[0].Close * 100 * 100) / 100,
                BenchmarkPerformance = Math.Round((benchmarkStockPrices[i].Close - benchmarkStockPrices[0].Close) / benchmarkStockPrices[0].Close * 100 * 100) / 100
            });
        }

        return stockPerformanceDTO;
    }
}