using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class addlinkralation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RelatedId",
                table: "Partners",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RelatedId",
                table: "OurServices",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Lang",
                table: "Contents",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RelatedId",
                table: "CaseStudies",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RelatedId",
                table: "Banners",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RelatedId",
                table: "About",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "RelatedId",
                table: "OurServices");

            migrationBuilder.DropColumn(
                name: "RelatedId",
                table: "CaseStudies");

            migrationBuilder.DropColumn(
                name: "RelatedId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "RelatedId",
                table: "About");

            migrationBuilder.AlterColumn<int>(
                name: "Lang",
                table: "Contents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
