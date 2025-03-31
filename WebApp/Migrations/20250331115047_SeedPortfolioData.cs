using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedPortfolioData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Portfolio",
                columns: new[] { "StockId", "UserId" },
                values: new object[,]
                {
                    { 1, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" },
                    { 2, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" },
                    { 3, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" },
                    { 4, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" },
                    { 1, "587b02bf-1685-4e18-a527-32c88216e78c" },
                    { 5, "587b02bf-1685-4e18-a527-32c88216e78c" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumns: new[] { "StockId", "UserId" },
                keyValues: new object[] { 1, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" });

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumns: new[] { "StockId", "UserId" },
                keyValues: new object[] { 2, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" });

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumns: new[] { "StockId", "UserId" },
                keyValues: new object[] { 3, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" });

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumns: new[] { "StockId", "UserId" },
                keyValues: new object[] { 4, "0aa2dc49-2fb8-4355-97e8-6048b21f4bfe" });

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumns: new[] { "StockId", "UserId" },
                keyValues: new object[] { 1, "587b02bf-1685-4e18-a527-32c88216e78c" });

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumns: new[] { "StockId", "UserId" },
                keyValues: new object[] { 5, "587b02bf-1685-4e18-a527-32c88216e78c" });
        }
    }
}
