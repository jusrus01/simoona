using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class UpdateEventParticipants3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOptionId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipantId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropIndex(
                name: "IX_EventOptionId",
                table: "EventParticipantEventOptions");

            migrationBuilder.CreateIndex(
                name: "IX_EventOption_Id",
                table: "EventParticipantEventOptions",
                column: "EventOption_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOptionId",
                table: "EventParticipantEventOptions",
                column: "EventOption_Id",
                principalTable: "EventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipantId",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOptionId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipantId",
                table: "EventParticipantEventOptions");

            migrationBuilder.DropIndex(
                name: "IX_EventOption_Id",
                table: "EventParticipantEventOptions");

            migrationBuilder.CreateIndex(
                name: "IX_EventOptionId",
                table: "EventParticipantEventOptions",
                column: "EventOption_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOptionId",
                table: "EventParticipantEventOptions",
                column: "EventOption_Id",
                principalTable: "EventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipantId",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id",
                principalTable: "EventParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
