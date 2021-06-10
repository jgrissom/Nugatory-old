using Microsoft.EntityFrameworkCore.Migrations;

namespace WordApi.Migrations
{
    public partial class renameId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WordColorId",
                table: "WordColors",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WordColors",
                newName: "WordColorId");
        }
    }
}
