using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class seedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4e797b0b-c669-4bf1-913c-a90fe951241f"), 0, "000625bb-cdde-4d1b-93d4-d5b735f4b7f7", "aleks@mail.com", false, false, false, null, "ALEKS@MAIL.COM", "ALEKS", "AQAAAAEAACcQAAAAEAMOrBacY5Nm/vVwnJJ0J73XBwNWFYLbX9/yu/BA5q0E+ABLN0bH81/qmh42japbaA==", null, false, "1567ab14-274e-4b66-8e76-bb1497ffea47", false, "aleks" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9622851e-71b4-439b-8d86-9e8cba27ca1e"), 0, "cfbfb8f9-85dd-4cc5-8446-0de044345bcc", "devora@mail.com", false, false, false, null, "DEVORA@MAIL.COM", "DEV", "AQAAAAEAACcQAAAAEHBULDLxJuVAEW9nBibYH1F21DOZBhOuIlJ67ASzVDEWXoaoRCtn6Zc89Nnxdi6ijw==", null, false, "54404ea0-5c65-46a0-bd43-4a9bc306c5e4", false, "Dev" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d273e367-ebf6-44b3-afa7-7759a0a579ee"), 0, "26f3672f-cb95-4860-ab62-9db1be189557", "gmihov@mail.com", false, false, false, null, "GMIHOV@MAIL.COM", "GEORGE", "AQAAAAEAACcQAAAAEFEpb6cmCFTo0C7XcdZmKZTD8AM+gs5U6MCraRESbGK9Di1gn8f6Jt6vJ3+IBQ1hHQ==", null, false, "c398a97c-fd49-4923-8128-f1eb3ac40d2e", false, "George" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4e797b0b-c669-4bf1-913c-a90fe951241f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9622851e-71b4-439b-8d86-9e8cba27ca1e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d273e367-ebf6-44b3-afa7-7759a0a579ee"));
        }
    }
}
