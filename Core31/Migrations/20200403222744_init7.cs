using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleUserId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_IdentityRoleUserId",
                table: "AspNetUserRoles",
                column: "IdentityRoleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_IdentityRoleUserId",
                table: "AspNetUserRoles",
                column: "IdentityRoleUserId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_IdentityRoleUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_IdentityRoleUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "IdentityRoleUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
