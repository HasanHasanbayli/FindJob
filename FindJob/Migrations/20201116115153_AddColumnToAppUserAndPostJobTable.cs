using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class AddColumnToAppUserAndPostJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostJobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    RequiredExperience = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Salary = table.Column<string>(nullable: true),
                    JobType = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: false),
                    Vacancies = table.Column<int>(nullable: false),
                    PostedDate = table.Column<DateTime>(nullable: false),
                    ExpiresDate = table.Column<DateTime>(nullable: false),
                    Interest = table.Column<int>(nullable: false),
                    Contacted = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostJobs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostJobs_AppUserId",
                table: "PostJobs",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostJobs");
        }
    }
}
