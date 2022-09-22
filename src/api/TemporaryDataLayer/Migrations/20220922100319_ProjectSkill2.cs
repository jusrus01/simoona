using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class ProjectSkill2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.AspNetUsers_ApplicationUser_Id",
                table: "ProjectApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.Projects_Project2_Id",
                table: "ProjectApplicationUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.AspNetUsers_ApplicationUser_Id",
                table: "ProjectApplicationUsers",
                column: "ApplicationUser_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.Projects_Project2_Id",
                table: "ProjectApplicationUsers",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.AspNetUsers_ApplicationUser_Id",
                table: "ProjectApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.Projects_Project2_Id",
                table: "ProjectApplicationUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.AspNetUsers_ApplicationUser_Id",
                table: "ProjectApplicationUsers",
                column: "ApplicationUser_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Project2ApplicationUser_dbo.Projects_Project2_Id",
                table: "ProjectApplicationUsers",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
