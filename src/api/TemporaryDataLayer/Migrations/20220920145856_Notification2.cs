using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Notification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.NotificationUsers_dbo.ApplicationUser_UserId",
                table: "NotificationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationUsers",
                table: "NotificationUsers");

            migrationBuilder.DropIndex(
                name: "IX_NotificationId",
                table: "NotificationUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Notifications",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationUsers",
                table: "NotificationUsers",
                columns: new[] { "NotificationId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "NotificationUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.NotificationUsers_dbo.AspNetUsers_UserId",
                table: "NotificationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.NotificationUsers_dbo.AspNetUsers_UserId",
                table: "NotificationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationUsers",
                table: "NotificationUsers");

            migrationBuilder.DropIndex(
                name: "IX_UserId",
                table: "NotificationUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Notifications",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationUsers",
                table: "NotificationUsers",
                columns: new[] { "UserId", "NotificationId" });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationId",
                table: "NotificationUsers",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.NotificationUsers_dbo.ApplicationUser_UserId",
                table: "NotificationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
