using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core31.Migrations
{
    public partial class init_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FootAbout",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FootContactInfo",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FootLinks",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FootLogo",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FootWechat",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialMedia",
                table: "Homes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                table: "Homes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Lang = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Displayed = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Lang = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Displayed = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseStudies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Lang = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Displayed = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    EditDate = table.Column<DateTime>(nullable: false),
                    AuthName = table.Column<string>(nullable: true),
                    Comment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Lang = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Displayed = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "CaseStudies");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FootAbout",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FootContactInfo",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FootLinks",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FootLogo",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FootWechat",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "SocialMedia",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Homes");
        }
    }
}
