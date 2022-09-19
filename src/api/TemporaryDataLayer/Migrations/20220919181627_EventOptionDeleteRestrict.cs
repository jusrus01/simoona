using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class EventOptionDeleteRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventOptions_dbo.Events_EventId",
                table: "EventOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventOptions_dbo.Events_EventId",
                table: "EventOptions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.EventOptions_dbo.Events_EventId",
                table: "EventOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventOptions_dbo.Events_EventId",
                table: "EventOptions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
