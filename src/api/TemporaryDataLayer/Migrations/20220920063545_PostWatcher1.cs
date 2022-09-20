using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class PostWatcher1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.PostWatchers_dbo.ApplicationUser_UserId",
                schema: "dbo",
                table: "PostWatchers");

            migrationBuilder.CreateIndex(
                name: "IX_PostId",
                schema: "dbo",
                table: "PostWatchers",
                column: "PostId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.PostWatchers_dbo.AspNetUsers_UserId",
                schema: "dbo",
                table: "PostWatchers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.PostWatchers_dbo.AspNetUsers_UserId",
                schema: "dbo",
                table: "PostWatchers");

            migrationBuilder.DropIndex(
                name: "IX_PostId",
                schema: "dbo",
                table: "PostWatchers");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.PostWatchers_dbo.ApplicationUser_UserId",
                schema: "dbo",
                table: "PostWatchers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
