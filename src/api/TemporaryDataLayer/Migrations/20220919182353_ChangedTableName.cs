using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ChangedTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipant_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipant_dbo.Events_EventId",
                table: "EventParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipant_EventParticipantId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.EventParticipant",
                table: "EventParticipant");

            migrationBuilder.RenameTable(
                name: "EventParticipant",
                newName: "EventParticipants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.EventParticipants",
                table: "EventParticipants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipantId",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipants_dbo.Events_EventId",
                table: "EventParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipantId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipants_dbo.Events_EventId",
                table: "EventParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.EventParticipants",
                table: "EventParticipants");

            migrationBuilder.RenameTable(
                name: "EventParticipants",
                newName: "EventParticipant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.EventParticipant",
                table: "EventParticipant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipant_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipant",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipant_dbo.Events_EventId",
                table: "EventParticipant",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipant_EventParticipantId",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
