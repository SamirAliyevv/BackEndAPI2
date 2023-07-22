using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppApi.Data.Migrations
{
    public partial class ImageFileintoProductsProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURl",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURl",
                table: "Products");
        }
    }
}
