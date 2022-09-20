using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class Event2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "EventTypeId",
                table: "Events",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowMaybeGoing",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowNotGoing",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Events",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EventRecurring",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId1",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxChoices",
                table: "Events",
                maxLength: 32767,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxParticipants",
                table: "Events",
                maxLength: 32767,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Events",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Offices",
                table: "Events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDeadline",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleUserId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EventId1",
                table: "EventParticipants",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventId1",
                table: "EventOptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId1",
                table: "Events",
                column: "EventTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeId",
                table: "Events",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Events",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsibleUserId",
                table: "Events",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_EventId1",
                table: "EventParticipants",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventOptions_EventId1",
                table: "EventOptions",
                column: "EventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventOptions_Events_EventId1",
                table: "EventOptions",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Events_EventId1",
                table: "EventParticipants",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId1",
                table: "Events",
                column: "EventTypeId1",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.Offices_OfficeId",
                table: "Events",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.Organizations_OrganizationId",
                table: "Events",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.ApplicationUser_ResponsibleUserId",
                table: "Events",
                column: "ResponsibleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventOptions_Events_EventId1",
                table: "EventOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Events_EventId1",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.Offices_OfficeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.Organizations_OrganizationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.Events_dbo.ApplicationUser_ResponsibleUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventTypeId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_OfficeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_ResponsibleUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_EventId1",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_EventOptions_EventId1",
                table: "EventOptions");

            migrationBuilder.DropColumn(
                name: "AllowMaybeGoing",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AllowNotGoing",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventRecurring",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventTypeId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MaxChoices",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MaxParticipants",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Offices",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationDeadline",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "EventOptions");

            migrationBuilder.AlterColumn<int>(
                name: "EventTypeId",
                table: "Events",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Events_dbo.EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
