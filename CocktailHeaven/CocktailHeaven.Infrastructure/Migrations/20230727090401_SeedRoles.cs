using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("56b69cff-f3a5-474e-8447-c5992ebbe8e3"), "5ac82c42-dd44-4856-b6c1-dfdc3c89be29", "Admin", "ADMIN" },
                    { new Guid("85f25772-3c9b-4cb8-a107-ed7b4c101e60"), "e49c61d0-1950-4e2c-80c0-9d2c4fbc2696", "CocktailEditor", "COCKTAILEDITOR" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("56b69cff-f3a5-474e-8447-c5992ebbe8e3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("85f25772-3c9b-4cb8-a107-ed7b4c101e60"));
        }
    }
}
