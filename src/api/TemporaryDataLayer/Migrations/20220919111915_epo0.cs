using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class epo0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipantEventOption_EventOptions_EventOption_Id",
                table: "EventParticipantEventOption");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipantEventOption_EventParticipant_EventParticipant_Id",
                table: "EventParticipantEventOption");

            migrationBuilder.RenameTable(
                name: "EventParticipantEventOption",
                newName: "EventParticipantEventOptions");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipantEventOption_EventOption_Id",
                table: "EventParticipantEventOptions",
                newName: "IX_EventOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOptionId",
                table: "EventParticipantEventOptions",
                column: "EventOption_Id",
                principalTable: "EventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipant_EventParticipantId",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOptionId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipant_EventParticipantId",
                table: "EventParticipantEventOptions");

            migrationBuilder.RenameTable(
                name: "EventParticipantEventOptions",
                newName: "EventParticipantEventOption");

            migrationBuilder.RenameIndex(
                name: "IX_EventOptionId",
                table: "EventParticipantEventOption",
                newName: "IX_EventParticipantEventOption_EventOption_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipantEventOption_EventOptions_EventOption_Id",
                table: "EventParticipantEventOption",
                column: "EventOption_Id",
                principalTable: "EventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipantEventOption_EventParticipant_EventParticipant_Id",
                table: "EventParticipantEventOption",
                column: "EventParticipant_Id",
                principalTable: "EventParticipant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
