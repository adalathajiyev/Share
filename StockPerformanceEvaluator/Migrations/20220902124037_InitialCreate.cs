using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockPerformanceEvaluator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "stock_price_sequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "stock_prices",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    price_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    high = table.Column<decimal>(type: "numeric", nullable: false),
                    low = table.Column<decimal>(type: "numeric", nullable: false),
                    close = table.Column<decimal>(type: "numeric", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_prices", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_prices");

            migrationBuilder.DropSequence(
                name: "stock_price_sequence");
        }
    }
}
