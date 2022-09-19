using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BlacklistUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlacklistUsers_CreatedBy",
                table: "BlacklistUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlacklistUsers_ModifiedBy",
                table: "BlacklistUsers");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "BlacklistUsers",
                column: "CreatedBy")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "BlacklistUsers",
                column: "ModifiedBy")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CreatedBy",
                table: "BlacklistUsers");

            migrationBuilder.DropIndex(
                name: "IX_ModifiedBy",
                table: "BlacklistUsers");

            migrationBuilder.CreateIndex(
                name: "IX_BlacklistUsers_CreatedBy",
                table: "BlacklistUsers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BlacklistUsers_ModifiedBy",
                table: "BlacklistUsers",
                column: "ModifiedBy");
        }
    }
}
