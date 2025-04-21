using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insomniac.Calculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CalculatorSetting",
                columns: new[] { "Id", "Calculator", "From", "Rate", "RateType", "To" },
                values: new object[,]
                {
                    { 1L, 1, 0m, 5m, 0, 199999m },
                    { 2L, 1, 200000m, 10000m, 1, null },
                    { 3L, 2, 0m, 17.5m, 0, null }
                });

            migrationBuilder.InsertData(
                table: "PostalCode",
                columns: new[] { "Id", "Calculator", "Code" },
                values: new object[,]
                {
                    { 1L, 1, "A100" },
                    { 2L, 2, "7000" },
                    { 3L, 2, "7001" },
                    { 4L, 2, "7002" },
                    { 5L, 1, "A101" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CalculatorSetting",
                keyColumn: "Id",
                keyValues: new object[] { 1L, 2L, 3L });

            migrationBuilder.DeleteData(
                table: "PostalCode",
                keyColumn: "Id",
                keyValues: new object[] { 1L, 2L, 3L, 4L, 5L });

        }
    }
}
