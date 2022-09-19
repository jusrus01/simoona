using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class UpdateEventParticipants1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "nci_wi_EventParticipants_CA1F6B4699FAB2347B166CEA9639C7E8",
                table: "EventParticipants",
                column: "IsDeleted")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "nci_wi_EventParticipants_CA1F6B4699FAB2347B166CEA9639C7E8",
                table: "EventParticipants");
        }
    }
}
