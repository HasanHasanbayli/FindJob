using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class RemoveColumnFromPostJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacted",
                table: "PostJobs");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "PostJobs");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "PostJobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Contacted",
                table: "PostJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Interest",
                table: "PostJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "PostJobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
