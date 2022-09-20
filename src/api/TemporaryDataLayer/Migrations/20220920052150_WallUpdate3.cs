using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.ApplicationUser_ResponsibleUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventTypeId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventTypeId1",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.AspNetUsers_ResponsibleUserId",
                table: "Events",
                column: "ResponsibleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.AspNetUsers_ResponsibleUserId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId1",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId1",
                table: "Events",
                column: "EventTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId1",
                table: "Events",
                column: "EventTypeId1",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.ApplicationUser_ResponsibleUserId",
                table: "Events",
                column: "ResponsibleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
