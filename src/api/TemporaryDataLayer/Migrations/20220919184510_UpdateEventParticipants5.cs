using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class UpdateEventParticipants5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipant_Id] FOREIGN KEY ([EventParticipant_Id",
                table: "EventParticipantEventOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipant_Id",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipant_Id",
                table: "EventParticipantEventOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipant_Id] FOREIGN KEY ([EventParticipant_Id",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
