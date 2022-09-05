using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace StockPerformanceEvaluator.Models;

public class StockPerformanceDTO
{
    public string InputSymbol { get; set; }

    public string BenchmarkSymbol { get; set; }

    public List<DailyPerformanceDTO> StockPerformance { get; set; }
}

public class DailyPerformanceDTO
{
    public string PriceDate { get; set; }

    public decimal InputPerformance { get; set; }

    public decimal BenchmarkPerformance { get; set; }
}

