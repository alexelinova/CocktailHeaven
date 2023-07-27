using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class SeedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"), new Guid("4e797b0b-c669-4bf1-913c-a90fe951241f") },
                    { new Guid("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"), new Guid("d273e367-ebf6-44b3-afa7-7759a0a579ee") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"), new Guid("4e797b0b-c669-4bf1-913c-a90fe951241f") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"), new Guid("d273e367-ebf6-44b3-afa7-7759a0a579ee") });
        }
    }
}
