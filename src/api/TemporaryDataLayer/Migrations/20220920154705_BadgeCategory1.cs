using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BadgeCategory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeCategoryKudosType_dbo.BadgeCategories_BadgeCategoryId",
                table: "BadgeCategoryKudosType");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeCategoryKudosType_dbo.KudosType_KudosTypeId",
                table: "BadgeCategoryKudosType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.BadgeCategoryKudosType",
                table: "BadgeCategoryKudosType");

            migrationBuilder.RenameTable(
                name: "BadgeCategoryKudosType",
                newName: "BadgeCategoryKudosTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.BadgeCategoryKudosTypes",
                table: "BadgeCategoryKudosTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.BadgeCategories_BadgeCategoryId",
                table: "BadgeCategoryKudosTypes",
                column: "BadgeCategoryId",
                principalTable: "BadgeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.KudosType_KudosTypeId",
                table: "BadgeCategoryKudosTypes",
                column: "KudosTypeId",
                principalTable: "KudosType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.BadgeCategories_BadgeCategoryId",
                table: "BadgeCategoryKudosTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.KudosType_KudosTypeId",
                table: "BadgeCategoryKudosTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.BadgeCategoryKudosTypes",
                table: "BadgeCategoryKudosTypes");

            migrationBuilder.RenameTable(
                name: "BadgeCategoryKudosTypes",
                newName: "BadgeCategoryKudosType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.BadgeCategoryKudosType",
                table: "BadgeCategoryKudosType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeCategoryKudosType_dbo.BadgeCategories_BadgeCategoryId",
                table: "BadgeCategoryKudosType",
                column: "BadgeCategoryId",
                principalTable: "BadgeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeCategoryKudosType_dbo.KudosType_KudosTypeId",
                table: "BadgeCategoryKudosType",
                column: "KudosTypeId",
                principalTable: "KudosType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
