using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteIt.Data.Migrations
{
    public partial class ChangePostName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Posts",
                newName: "Title");
        }
    }
}
