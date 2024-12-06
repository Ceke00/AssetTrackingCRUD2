using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTrackingCRUD2.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAssetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "brand", "country", "model", "purchased_date", "asset_type", "price_currency", "price_value" },
                values: new object[,]
                {
                    { 2, "Dell", "germany", "XPS 13", new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "computer", "EUR", 1500m },
                    { 3, "HP", "sweden", "Spectre x360", new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "computer", "SEK", 1400m },
                    { 4, "Apple", "usa", "MacBook Air", new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "computer", "USD", 1200m },
                    { 5, "Lenovo", "usa", "ThinkPad X1", new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "computer", "USD", 1600m },
                    { 6, "Asus", "sweden", "ZenBook", new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "computer", "SEK", 1300m },
                    { 7, "Samsung", "sweden", "Galaxy S21", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phone", "SEK", 900m },
                    { 8, "Google", "germany", "Pixel 5", new DateTime(2020, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "phone", "EUR", 700m },
                    { 9, "OnePlus", "usa", "8T", new DateTime(2022, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "phone", "USD", 600m },
                    { 10, "Sony", "sweden", "Xperia 1 II", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phone", "SEK", 950m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
