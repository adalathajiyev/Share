using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace StockPerformanceEvaluator.Database.Entities;

[Table("stock_prices")]
public class StockPriceEntity : BaseEntity
{
    [Column("price_date")]
    public DateTime PriceDate { get; set; }

    [Column("symbol")]
    public string Symbol { get; set; }

    [Column("high")]
    public decimal High { get; set; }

    [Column("low")]
    public decimal Low { get; set; }

    [Column("close")]
    public decimal Close { get; set; }

    [Column("volume")]
    public decimal Volume { get; set; }
}
