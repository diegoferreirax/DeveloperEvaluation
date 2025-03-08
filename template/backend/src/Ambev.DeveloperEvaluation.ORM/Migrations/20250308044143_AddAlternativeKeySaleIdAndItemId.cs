using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddAlternativeKeySaleIdAndItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItem");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SaleItem_SaleId_ItemId",
                table: "SaleItem",
                columns: new[] { "SaleId", "ItemId" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3b765f33-6d77-4da6-906f-511b1e2d009d"), "Maria" },
                    { new Guid("7bda9e04-f297-42f5-bd3b-c0fed49eacd4"), "Alberto" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Product", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("2ccb7715-03fc-447f-9632-73a8a8bcc816"), "Patagônia", 8.99m },
                    { new Guid("5818a8f0-cd7c-4a5e-a7f2-a99507e9260d"), "Brahma", 4.69m },
                    { new Guid("5ad99f20-db03-4d06-b539-28ece3792303"), "Skol", 4m },
                    { new Guid("85c1e99d-4311-4fba-95c3-f327e34d3020"), "Brahma DuploMaute", 5.39m },
                    { new Guid("c06a6875-7737-4558-a013-6acfb4e705c7"), "Patagônia IPA", 8.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SaleItem_SaleId_ItemId",
                table: "SaleItem");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("3b765f33-6d77-4da6-906f-511b1e2d009d"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("7bda9e04-f297-42f5-bd3b-c0fed49eacd4"));

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: new Guid("2ccb7715-03fc-447f-9632-73a8a8bcc816"));

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: new Guid("5818a8f0-cd7c-4a5e-a7f2-a99507e9260d"));

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: new Guid("5ad99f20-db03-4d06-b539-28ece3792303"));

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: new Guid("85c1e99d-4311-4fba-95c3-f327e34d3020"));

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: new Guid("c06a6875-7737-4558-a013-6acfb4e705c7"));

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItem",
                column: "SaleId");
        }
    }
}
