using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class WallMember4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "nci_wi_WallMembers_6C8CE6B55B79BC00FDA53D9B579C2EFA",
                table: "WallMembers");

            migrationBuilder.CreateIndex(
                name: "nci_wi_WallMembers_6C8CE6B55B79BC00FDA53D9B579C2EFA",
                table: "WallMembers",
                columns: new[] { "IsDeleted", "UserId" })
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "nci_wi_WallMembers_6C8CE6B55B79BC00FDA53D9B579C2EFA",
                table: "WallMembers");

            migrationBuilder.CreateIndex(
                name: "nci_wi_WallMembers_6C8CE6B55B79BC00FDA53D9B579C2EFA",
                table: "WallMembers",
                column: "IsDeleted")
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
