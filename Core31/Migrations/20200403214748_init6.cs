using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SDHCUserId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_SDHCUserId",
                table: "AspNetUserRoles",
                column: "SDHCUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_SDHCUserId",
                table: "AspNetUserRoles",
                column: "SDHCUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_SDHCUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_SDHCUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "SDHCUserId",
                table: "AspNetUserRoles");
        }
    }
}
