using Microsoft.EntityFrameworkCore.Migrations;

namespace FindJob.Migrations
{
    public partial class AddPdfFileColumnToAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfFile",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfFile",
                table: "AspNetUsers");
        }
    }
}
