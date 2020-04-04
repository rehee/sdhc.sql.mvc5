using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_IdentityRoleUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_SDHCUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_IdentityRoleUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_SDHCUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "IdentityRoleUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "SDHCUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleUserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDHCUserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_IdentityRoleUserId",
                table: "AspNetUserRoles",
                column: "IdentityRoleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_SDHCUserId",
                table: "AspNetUserRoles",
                column: "SDHCUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_IdentityRoleUserId",
                table: "AspNetUserRoles",
                column: "IdentityRoleUserId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_SDHCUserId",
                table: "AspNetUserRoles",
                column: "SDHCUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
