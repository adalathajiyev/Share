using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockPerformanceEvaluator.Models;

namespace StockPerformanceEvaluator.Services.ApiClientService;

public class AlphavantageApiClient : IApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AlphavantageApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<Dictionary<string, DailyPrices>> getDailyStockPricesAsync(string symbol)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.GetAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey=SF1S394MSWL6S98F");

        string responseBody = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<DailyPricesResponse>(responseBody).DailyPrices;
    }
}