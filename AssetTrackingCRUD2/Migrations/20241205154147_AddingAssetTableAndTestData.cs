using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTrackingCRUD2.Migrations
{
    /// <inheritdoc />
    public partial class AddingAssetTableAndTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchased_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price_currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    asset_type = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "brand", "country", "model", "purchased_date", "asset_type", "price_currency", "price_value" },
                values: new object[] { 1, "Apple", "germany", "iPhone 12", new DateTime(2020, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "phone", "EUR", 1020m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
