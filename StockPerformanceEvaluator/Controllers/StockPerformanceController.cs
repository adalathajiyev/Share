using System.Runtime;
using Microsoft.AspNetCore.Mvc;
using StockPerformanceEvaluator.Models;
using StockPerformanceEvaluator.Services;
using StockPerformanceEvaluator.Services.ApiClientService;

namespace StockPerformanceEvaluator.Controllers;

[ApiController]
[Route("api/stock/performance")]
public class StockPerformanceController : ControllerBase
{
    private readonly ILogger<StockPerformanceController> _logger;
    private readonly IStockPriceService _stockPriceService;

    public StockPerformanceController(ILogger<StockPerformanceController> logger, IStockPriceService stockPriceService)
    {
        _logger = logger;
        _stockPriceService = stockPriceService;
    }

    [HttpGet("daily")]
    public async Task<StockPerformanceDTO> GetDailyPerformance(string symbol)
    {
        if(symbol.Length < 2)
        {
            throw new BadHttpRequestException("symbol should be more than 1 charachter");
        }
        
        return await _stockPriceService.EvaluateDailyStockPerformance(symbol); ;
    }
}

