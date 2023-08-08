using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class ChangedSpellingForCocktailEditorRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"),
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58f737d9-0650-42c4-a38f-86f0103133c4", "Cocktail Editor", "COCKTAIL EDITOR" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"),
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "780a8497-9cf2-4134-8950-93f6dd3ac42c", "CocktailEditor", "COCKTAILEDITOR" });
        }
    }
}
