using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPerformanceEvaluator.Database.Entities;

public abstract class BaseEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

