using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace old_stuff_exchange_v2.Migrations
{
    public partial class authen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuidingId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Role");

            migrationBuilder.AddColumn<Guid>(
                name: "BuidingId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
