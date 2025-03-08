using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SaleItem_SaleId_ItemId",
                table: "SaleItem");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItem",
                column: "SaleId");
        }
    }
}
