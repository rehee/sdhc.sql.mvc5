using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KKK",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeChatOpenId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WeChatOpenId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "KKK",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
