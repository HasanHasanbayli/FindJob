using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class AddColumnToBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "PostJobs");

            migrationBuilder.AddColumn<string>(
                name: "FontDescription",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FontDescription",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "PostJobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
