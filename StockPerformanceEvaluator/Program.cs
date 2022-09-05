using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using StockPerformanceEvaluator.Database;
using StockPerformanceEvaluator.Extensions;
using StockPerformanceEvaluator.Services;
using StockPerformanceEvaluator.Services.ApiClientService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IApiClient, AlphavantageApiClient>();

builder.Services.AddTransient<IStockPriceService, StockPriceService>();

builder.Services.AddTransient<IStockPriceUpdateService, StockPriceUpdateService>();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Prepare configuration builder
IConfigurationRoot configuration = new ConfigurationBuilder()
  .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
  .AddJsonFile("appsettings.json", optional: false)
  .AddJsonFile($"appsettings.Development.json", optional: false)
  .Build();

builder.Services.AddDbContext<StockPerformanceDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("StockPerformanceDBContext")));

var app = builder.Build();

await app.MigrateDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();