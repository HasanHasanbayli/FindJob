using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class AddImageColumnToPostJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "PostJobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "PostJobs");
        }
    }
}
