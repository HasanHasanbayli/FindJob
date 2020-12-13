using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class CreateCitiesAndCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "PostJobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "PostJobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostJobs_CityId",
                table: "PostJobs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PostJobs_JobCategoryId",
                table: "PostJobs",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostJobs_Cities_CityId",
                table: "PostJobs",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostJobs_JobCategories_JobCategoryId",
                table: "PostJobs",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostJobs_Cities_CityId",
                table: "PostJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_PostJobs_JobCategories_JobCategoryId",
                table: "PostJobs");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_PostJobs_CityId",
                table: "PostJobs");

            migrationBuilder.DropIndex(
                name: "IX_PostJobs_JobCategoryId",
                table: "PostJobs");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "PostJobs");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "PostJobs");
        }
    }
}
