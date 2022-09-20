using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Notification3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationUsers",
                table: "NotificationUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.NotificationUsers",
                table: "NotificationUsers",
                columns: new[] { "NotificationId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "ix_notification_IsAlreadySeen",
                table: "NotificationUsers",
                column: "IsAlreadySeen")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationId",
                table: "NotificationUsers",
                column: "NotificationId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.NotificationUsers",
                table: "NotificationUsers");

            migrationBuilder.DropIndex(
                name: "ix_notification_IsAlreadySeen",
                table: "NotificationUsers");

            migrationBuilder.DropIndex(
                name: "IX_NotificationId",
                table: "NotificationUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationUsers",
                table: "NotificationUsers",
                columns: new[] { "NotificationId", "UserId" });
        }
    }
}
