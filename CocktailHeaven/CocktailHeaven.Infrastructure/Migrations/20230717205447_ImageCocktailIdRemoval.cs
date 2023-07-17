using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class ImageCocktailIdRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Image_ImageId",
                table: "Cocktails");

            migrationBuilder.DropColumn(
                name: "CocktailId",
                table: "Image");

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Image_ImageId",
                table: "Cocktails",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Image_ImageId",
                table: "Cocktails");

            migrationBuilder.AddColumn<int>(
                name: "CocktailId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Image_ImageId",
                table: "Cocktails",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
