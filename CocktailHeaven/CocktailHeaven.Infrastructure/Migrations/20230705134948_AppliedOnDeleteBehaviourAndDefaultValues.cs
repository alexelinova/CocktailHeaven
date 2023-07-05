using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    public partial class AppliedOnDeleteBehaviourAndDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_AspNetUsers_ApplicationUserId",
                table: "Cocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Categories_CategoryId",
                table: "Cocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Cocktails_CocktailId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_ApplicationUserId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCollections_AspNetUsers_ApplicationUserId",
                table: "UserCollections");

            migrationBuilder.DropIndex(
                name: "IX_Image_CocktailId",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "UserCollections",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCollections_ApplicationUserId",
                table: "UserCollections",
                newName: "IX_UserCollections_AddedByUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Rating",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_ApplicationUserId",
                table: "Rating",
                newName: "IX_Rating_AddedByUserId");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Image",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Cocktails",
                newName: "AddedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cocktails_ApplicationUserId",
                table: "Cocktails",
                newName: "IX_Cocktails_AddedByUserId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Rating",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Image",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Cocktails",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Cocktails_ImageId",
                table: "Cocktails",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_AspNetUsers_AddedByUserId",
                table: "Cocktails",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Categories_CategoryId",
                table: "Cocktails",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Image_ImageId",
                table: "Cocktails",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_AddedByUserId",
                table: "Rating",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCollections_AspNetUsers_AddedByUserId",
                table: "UserCollections",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_AspNetUsers_AddedByUserId",
                table: "Cocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Categories_CategoryId",
                table: "Cocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Image_ImageId",
                table: "Cocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_AddedByUserId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCollections_AspNetUsers_AddedByUserId",
                table: "UserCollections");

            migrationBuilder.DropIndex(
                name: "IX_Cocktails_ImageId",
                table: "Cocktails");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "UserCollections",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCollections_AddedByUserId",
                table: "UserCollections",
                newName: "IX_UserCollections_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "Rating",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_AddedByUserId",
                table: "Rating",
                newName: "IX_Rating_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Image",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "AddedByUserId",
                table: "Cocktails",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cocktails_AddedByUserId",
                table: "Cocktails",
                newName: "IX_Cocktails_ApplicationUserId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Rating",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Image",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Cocktails",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Image_CocktailId",
                table: "Image",
                column: "CocktailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_AspNetUsers_ApplicationUserId",
                table: "Cocktails",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Categories_CategoryId",
                table: "Cocktails",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Cocktails_CocktailId",
                table: "Image",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_ApplicationUserId",
                table: "Rating",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCollections_AspNetUsers_ApplicationUserId",
                table: "UserCollections",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
