using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace old_stuff_exchange_v2.Migrations
{
    public partial class updatebigdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "Wallet",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Wallet",
                newName: "Desription");

            migrationBuilder.RenameColumn(
                name: "Remaining",
                table: "Transaction",
                newName: "Balance");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Transaction",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Coin",
                table: "Deposit",
                newName: "CoinExchange");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 298, DateTimeKind.Local).AddTicks(8678),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 298, DateTimeKind.Local).AddTicks(8279),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 292, DateTimeKind.Local).AddTicks(6942),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 297, DateTimeKind.Local).AddTicks(4625),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 296, DateTimeKind.Local).AddTicks(4537),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<Guid>(
                name: "UserBought",
                table: "Post",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 298, DateTimeKind.Local).AddTicks(5104),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBought",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Wallet",
                newName: "Properties");

            migrationBuilder.RenameColumn(
                name: "Desription",
                table: "Wallet",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transaction",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Transaction",
                newName: "Remaining");

            migrationBuilder.RenameColumn(
                name: "CoinExchange",
                table: "Deposit",
                newName: "Coin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 298, DateTimeKind.Local).AddTicks(8678));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 298, DateTimeKind.Local).AddTicks(8279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 292, DateTimeKind.Local).AddTicks(6942));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 297, DateTimeKind.Local).AddTicks(4625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 296, DateTimeKind.Local).AddTicks(4537));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 9, 13, 28, 51, 298, DateTimeKind.Local).AddTicks(5104));
        }
    }
}
