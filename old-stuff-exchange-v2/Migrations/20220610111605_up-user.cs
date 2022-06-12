using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace old_stuff_exchange_v2.Migrations
{
    public partial class upuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(7259),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2584));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(6929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2200));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 247, DateTimeKind.Local).AddTicks(5742),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 265, DateTimeKind.Local).AddTicks(2745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(9416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(4771));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 251, DateTimeKind.Local).AddTicks(5294),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 269, DateTimeKind.Local).AddTicks(398));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(3977),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 270, DateTimeKind.Local).AddTicks(8917));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(7259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(2200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(6929));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "User",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 265, DateTimeKind.Local).AddTicks(2745),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 247, DateTimeKind.Local).AddTicks(5742));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 271, DateTimeKind.Local).AddTicks(4771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 269, DateTimeKind.Local).AddTicks(398),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 251, DateTimeKind.Local).AddTicks(5294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Deposit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 10, 18, 12, 33, 270, DateTimeKind.Local).AddTicks(8917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 10, 18, 16, 5, 253, DateTimeKind.Local).AddTicks(3977));
        }
    }
}
