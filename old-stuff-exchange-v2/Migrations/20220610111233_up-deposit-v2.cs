using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace old_stuff_exchange_v2.Migrations
{
    public partial class updepositv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinExchange",
                table: "Deposit");

            migrationBuilder.DropColumn(
                name: "RemainingCoinInWallet",
                table: "Deposit");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transaction",
                newName: "CoinExchange");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 30, DateTimeKind.Local).AddTicks(1442));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 30, DateTimeKind.Local).AddTicks(1113));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 265, DateTimeKind.Local).AddTicks(2745),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 24, DateTimeKind.Local).AddTicks(851));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(4771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 30, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 269, DateTimeKind.Local).AddTicks(398),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 27, DateTimeKind.Local).AddTicks(7681));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 270, DateTimeKind.Local).AddTicks(8917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 29, DateTimeKind.Local).AddTicks(8167));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoinExchange",
                table: "Transaction",
                newName: "Amount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 30, DateTimeKind.Local).AddTicks(1442),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2584));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 30, DateTimeKind.Local).AddTicks(1113),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 24, DateTimeKind.Local).AddTicks(851),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 265, DateTimeKind.Local).AddTicks(2745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 30, DateTimeKind.Local).AddTicks(3010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(4771));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 27, DateTimeKind.Local).AddTicks(7681),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 269, DateTimeKind.Local).AddTicks(398));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 14, 34, 45, 29, DateTimeKind.Local).AddTicks(8167),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 270, DateTimeKind.Local).AddTicks(8917));

            migrationBuilder.AddColumn<decimal>(
                name: "CoinExchange",
                table: "Deposit",
                type: "decimal(10,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingCoinInWallet",
                table: "Deposit",
                type: "decimal(10,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
