using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class ImageIdNotAllowingNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cocktails_ImageId",
                table: "Cocktails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Cocktails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cocktails_ImageId",
                table: "Cocktails",
                column: "ImageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cocktails_ImageId",
                table: "Cocktails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Cocktails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Cocktails_ImageId",
                table: "Cocktails",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");
        }
    }
}
