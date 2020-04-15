using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class update_ourservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Displaied",
                table: "OurServices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "OurServices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "OurServices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Lang",
                table: "OurServices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Displaied",
                table: "OurServices");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "OurServices");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "OurServices");

            migrationBuilder.DropColumn(
                name: "Lang",
                table: "OurServices");
        }
    }
}
