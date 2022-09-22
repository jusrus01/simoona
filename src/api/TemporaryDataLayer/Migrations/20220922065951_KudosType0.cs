using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class KudosType0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.KudosType_KudosTypeId",
                table: "BadgeCategoryKudosTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.KudosType",
                table: "KudosType");

            migrationBuilder.RenameTable(
                name: "KudosType",
                newName: "KudosTypes");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "KudosTypes",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "KudosTypes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "KudosTypes",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "KudosTypes",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "KudosTypes",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KudosTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.KudosTypes",
                table: "KudosTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.KudosTypes_KudosTypeId",
                table: "BadgeCategoryKudosTypes",
                column: "KudosTypeId",
                principalTable: "KudosTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.KudosTypes_KudosTypeId",
                table: "BadgeCategoryKudosTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.KudosTypes",
                table: "KudosTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KudosTypes");

            migrationBuilder.RenameTable(
                name: "KudosTypes",
                newName: "KudosType");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "KudosType",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "KudosType",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "KudosType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "KudosType",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "KudosType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.KudosType",
                table: "KudosType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BadgeCategoryKudosTypes_dbo.KudosType_KudosTypeId",
                table: "BadgeCategoryKudosTypes",
                column: "KudosTypeId",
                principalTable: "KudosType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
