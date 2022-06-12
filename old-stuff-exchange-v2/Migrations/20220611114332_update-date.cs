using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace old_stuff_exchange_v2.Migrations
{
    public partial class updatedate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 388, DateTimeKind.Local).AddTicks(4898),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(7259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 388, DateTimeKind.Local).AddTicks(4555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(6929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 382, DateTimeKind.Local).AddTicks(2919),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 247, DateTimeKind.Local).AddTicks(5742));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 388, DateTimeKind.Local).AddTicks(7258),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 386, DateTimeKind.Local).AddTicks(3367),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 386, DateTimeKind.Local).AddTicks(2805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 251, DateTimeKind.Local).AddTicks(5294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(3977));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(7259),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 388, DateTimeKind.Local).AddTicks(4898));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(6929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 388, DateTimeKind.Local).AddTicks(4555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 247, DateTimeKind.Local).AddTicks(5742),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 382, DateTimeKind.Local).AddTicks(2919));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(9416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 388, DateTimeKind.Local).AddTicks(7258));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 386, DateTimeKind.Local).AddTicks(3367));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 251, DateTimeKind.Local).AddTicks(5294),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 18, 43, 32, 386, DateTimeKind.Local).AddTicks(2805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(3977),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
