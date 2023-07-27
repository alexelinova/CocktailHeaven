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
					{ new Guid("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"), "78c93863-bd2d-4c33-82af-cc5c35f610d1", "Admin", "ADMIN" },
					{ new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"), "f1eed44f-a763-48ce-ab50-ef222994b358", "CocktailEditor", "COCKTAILEDITOR" }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
		        table: "AspNetRoles",
		        keyColumn: "Id",
		        keyValue: new Guid("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"));

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"));
		}
	}
}
