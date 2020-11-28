using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class AddColumnToPostJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "PostJobs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostJobs_AppUserId",
                table: "PostJobs",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostJobs_AspNetUsers_AppUserId",
                table: "PostJobs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostJobs_AspNetUsers_AppUserId",
                table: "PostJobs");

            migrationBuilder.DropIndex(
                name: "IX_PostJobs_AppUserId",
                table: "PostJobs");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "PostJobs");
        }
    }
}
