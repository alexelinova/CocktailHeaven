using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class NewColumnToImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalURL",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalURL",
                table: "Image");
        }
    }
}
