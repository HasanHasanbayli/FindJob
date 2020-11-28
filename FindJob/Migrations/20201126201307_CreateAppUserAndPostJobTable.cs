using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class CreateAppUserAndPostJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "PostJobs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppUserPostJobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFavorite = table.Column<bool>(nullable: false),
                    IsContacted = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    PostJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserPostJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserPostJobs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserPostJobs_PostJobs_PostJobId",
                        column: x => x.PostJobId,
                        principalTable: "PostJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPostJobs_AppUserId",
                table: "AppUserPostJobs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPostJobs_PostJobId",
                table: "AppUserPostJobs",
                column: "PostJobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserPostJobs");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "PostJobs");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "PostJobs",
                type: "nvarchar(450)",
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
    }
}
