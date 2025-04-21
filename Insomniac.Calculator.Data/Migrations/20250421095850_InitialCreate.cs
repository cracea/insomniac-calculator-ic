using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insomniac.Calculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatorHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Income = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tax = table.Column<decimal>(type: "TEXT", nullable: false),
                    Calculator = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatorHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculatorSetting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Calculator = table.Column<int>(type: "INTEGER", nullable: false),
                    RateType = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false),
                    From = table.Column<decimal>(type: "TEXT", nullable: false),
                    To = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatorSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Calculator = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostalCode_Code",
                table: "PostalCode",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatorHistory");

            migrationBuilder.DropTable(
                name: "CalculatorSetting");

            migrationBuilder.DropTable(
                name: "PostalCode");
        }
    }
}
