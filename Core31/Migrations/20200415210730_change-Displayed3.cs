using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class changeDisplayed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Home2",
                table: "Contents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Home2",
                table: "Contents");
        }
    }
}
