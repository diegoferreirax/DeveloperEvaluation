using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AlterNumericTypePrecisions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SaleId",
                table: "SaleItem",
                type: "UUID",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemId",
                table: "SaleItem",
                type: "UUID",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "SaleItem",
                type: "numeric(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SaleItem",
                type: "UUID",
                nullable: false,
                defaultValueSql: "GEN_RANDOM_UUID()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Sale",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sale",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Sale",
                type: "UUID",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Sale",
                type: "UUID",
                nullable: false,
                defaultValueSql: "GEN_RANDOM_UUID()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Item",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Item",
                type: "UUID",
                nullable: false,
                defaultValueSql: "GEN_RANDOM_UUID()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Customer",
                type: "UUID",
                nullable: false,
                defaultValueSql: "GEN_RANDOM_UUID()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SaleId",
                table: "SaleItem",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UUID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemId",
                table: "SaleItem",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UUID");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "SaleItem",
                type: "numeric(7,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SaleItem",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "UUID",
                oldDefaultValueSql: "GEN_RANDOM_UUID()");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Sale",
                type: "numeric(7,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sale",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Sale",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UUID");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Sale",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "UUID",
                oldDefaultValueSql: "GEN_RANDOM_UUID()");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Item",
                type: "numeric(7,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Item",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "UUID",
                oldDefaultValueSql: "GEN_RANDOM_UUID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Customer",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "UUID",
                oldDefaultValueSql: "GEN_RANDOM_UUID()");
        }
    }
}
