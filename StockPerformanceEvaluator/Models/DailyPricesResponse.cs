using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockPerformanceEvaluator.Models;

public class DailyPricesResponse
{
    [JsonProperty(PropertyName = "Time Series (Daily)")]
    public Dictionary<string, DailyPrices> DailyPrices { get; set; }
}

public class DailyPrices
{
    [JsonProperty(PropertyName = "1. open")]
    public decimal Open { get; set; }

    [JsonProperty(PropertyName = "2. high")]
    public decimal High { get; set; }

    [JsonProperty(PropertyName = "3. low")]
    public decimal Low { get; set; }

    [JsonProperty(PropertyName = "4. close")]
    public decimal Close { get; set; }

    [JsonProperty(PropertyName = "5. volume")]
    public decimal Volume { get; set; }

}
