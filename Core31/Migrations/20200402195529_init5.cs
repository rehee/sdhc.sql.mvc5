using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FF",
                table: "Contents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FF",
                table: "Contents");
        }
    }
}
