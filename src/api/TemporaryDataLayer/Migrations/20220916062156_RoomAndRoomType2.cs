using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class RoomAndRoomType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.RoomTypes_dbo.Organizations_OrganizationId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_OrganizationId",
                table: "RoomTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "RoomTypes",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsWorkingRoom",
                table: "RoomTypes",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RoomTypes",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "RoomTypes",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "#FFFFFF");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "RoomTypes",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.RoomTypes_dbo.Organizations_OrganizationId",
                table: "RoomTypes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.RoomTypes_dbo.Organizations_OrganizationId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "RoomTypes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "IsWorkingRoom",
                table: "RoomTypes",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RoomTypes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "RoomTypes",
                nullable: true,
                defaultValue: "#FFFFFF",
                oldClrType: typeof(string),
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_OrganizationId",
                table: "RoomTypes",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.RoomTypes_dbo.Organizations_OrganizationId",
                table: "RoomTypes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
