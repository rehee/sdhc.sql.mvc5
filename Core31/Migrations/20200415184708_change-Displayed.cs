using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class changeDisplayed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Displaied",
                table: "OurServices");

            migrationBuilder.AddColumn<bool>(
                name: "Displayed",
                table: "OurServices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Displayed",
                table: "OurServices");

            migrationBuilder.AddColumn<bool>(
                name: "Displaied",
                table: "OurServices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
