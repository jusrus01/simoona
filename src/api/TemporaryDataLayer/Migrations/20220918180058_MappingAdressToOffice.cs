using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class MappingAdressToOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_Building",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Offices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Building",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Offices");
        }
    }
}
