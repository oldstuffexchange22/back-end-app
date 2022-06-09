using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace old_stuff_exchange_v2.Migrations
{
    public partial class updatedeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepositId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletElectricName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Coin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingCoinInWallet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposit_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DepositId",
                table: "Transaction",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_UserId",
                table: "Deposit",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Deposit_DepositId",
                table: "Transaction",
                column: "DepositId",
                principalTable: "Deposit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Deposit_DepositId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_DepositId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "DepositId",
                table: "Transaction");
        }
    }
}
