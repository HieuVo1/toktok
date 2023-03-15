using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace toktok_api.Migrations
{
    public partial class changeFieldImageToImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Movies",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Movies",
                newName: "Image");
        }
    }
}
