using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class add_banner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Banners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Banners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Banners");
        }
    }
}
