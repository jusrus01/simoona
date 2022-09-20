using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporaryDataLayer.Migrations
{
    public partial class BadgeType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BadgeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BadgeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    ShortName = table.Column<string>(maxLength: 64, nullable: false),
                    HostName = table.Column<string>(maxLength: 50, nullable: true),
                    HasRestrictedAccess = table.Column<bool>(nullable: false, defaultValue: false),
                    WelcomeEmail = table.Column<string>(maxLength: 10000, nullable: false, defaultValue: "<p style=\"text - align:center; font - size:14px; font - weight:400; margin: 0 0 0 0; \">Administrator has confirmed your registration</p>"),
                    RequiresUserConfirmation = table.Column<bool>(nullable: false, defaultValue: false),
                    CalendarId = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true, defaultValue: "FLE Standard Time"),
                    CultureCode = table.Column<string>(nullable: true, defaultValue: "en-US"),
                    BookAppAuthorizationGuid = table.Column<string>(nullable: true),
                    AuthenticationProviders = table.Column<string>(nullable: true),
                    KudosYearlyMultipliers = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BadgeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageSmallUrl = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    BadgeCategoryId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BadgeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BadgeTypes_dbo.BadgeCategories_BadgeCategoryId",
                        column: x => x.BadgeCategoryId,
                        principalTable: "BadgeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsSingleJoin = table.Column<bool>(nullable: false),
                    SingleJoinGroupName = table.Column<string>(maxLength: 100, nullable: true),
                    SendWeeklyReminders = table.Column<bool>(nullable: false, defaultValue: false),
                    IsShownWithMainEvents = table.Column<bool>(nullable: false, defaultValue: true),
                    SendEmailToManager = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.EventTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.EventTypes_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false, defaultValue: 0),
                    Priority = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ExternalLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.ExternalLinks_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilterPresets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    ForPage = table.Column<int>(nullable: false),
                    Preset = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.FilterPresets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.FilterPresets_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.JobPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.JobPositions_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lotteries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    EntryFee = table.Column<int>(nullable: false),
                    IsRefundFailed = table.Column<bool>(nullable: false, defaultValue: false),
                    Images = table.Column<string>(nullable: true),
                    GiftedTicketLimit = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Lotteries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Lotteries_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOrganizations",
                columns: table => new
                {
                    Module_Id = table.Column<int>(nullable: false),
                    Organization_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ModuleOrganizations", x => new { x.Module_Id, x.Organization_Id });
                    table.ForeignKey(
                        name: "FK_dbo.ShroomsModuleOrganizations_dbo.ShroomsModules_ShroomsModule_Id",
                        column: x => x.Module_Id,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ShroomsModuleOrganizations_dbo.Organizations_Organization_Id",
                        column: x => x.Organization_Id,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PictureId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false, defaultValue: 0),
                    Sources_PostId = table.Column<int>(nullable: false),
                    Sources_EventId = table.Column<string>(nullable: true),
                    Sources_ProjectId = table.Column<string>(nullable: true),
                    Sources_WallId = table.Column<int>(nullable: false),
                    Sources = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Notifications_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_Building = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Offices_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentPageId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Pages_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Pages_dbo.Pages_ParentPageId",
                        column: x => x.ParentPageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ThumbData = table.Column<byte[]>(nullable: true),
                    MiniThumbData = table.Column<byte[]>(nullable: true),
                    Width = table.Column<int>(nullable: true),
                    Height = table.Column<int>(nullable: true),
                    ThumbWidth = table.Column<int>(nullable: true),
                    ThumbHeight = table.Column<int>(nullable: true),
                    MiniThumbWidth = table.Column<int>(nullable: true),
                    MiniThumbHeight = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Pictures_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IconId = table.Column<string>(nullable: true),
                    IsWorkingRoom = table.Column<bool>(nullable: false, defaultValue: false),
                    Color = table.Column<string>(maxLength: 7, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.RoomTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.RoomTypes_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SyncTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SyncTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.SyncTokens_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VacationPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.VacationPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.VacationPages_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Walls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true, defaultValue: "wall-default.png"),
                    IsHiddenFromAllWalls = table.Column<bool>(nullable: false),
                    AddForNewUsers = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Access = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Walls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Walls_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    PictureId = table.Column<string>(nullable: true),
                    Picture_Id = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Floors_dbo.Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Floors_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Floors_dbo.Pictures_Picture_Id",
                        column: x => x.Picture_Id,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    FloorId = table.Column<int>(nullable: true),
                    RoomTypeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Rooms_dbo.Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Rooms_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Rooms_dbo.RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "BlacklistUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BlacklistUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BlacklistUsers_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    TakenFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    Returned = table.Column<DateTime>(type: "datetime", nullable: true),
                    BookOfficeId = table.Column<int>(nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BookLogs_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    Url = table.Column<string>(maxLength: 2000, nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Note = table.Column<string>(maxLength: 9000, nullable: true),
                    BookCoverUrl = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Books_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookOffices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.BookOffices_dbo.Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.BookOffices_dbo.Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.BookOffices_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Likes = table.Column<string>(nullable: true, defaultValue: "{}"),
                    MessageBody = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: false, defaultValue: 0),
                    Images = table.Column<string>(nullable: true),
                    LastEdit = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    IsHidden = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    ApplicationUserId = table.Column<string>(nullable: false, defaultValue: ""),
                    AttendStatus = table.Column<int>(nullable: false, defaultValue: 1),
                    AttendComment = table.Column<string>(nullable: true),
                    EventId1 = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.EventParticipants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    OrganizationId = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RegistrationDeadline = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2016, 5, 11, 11, 57, 2, 755, DateTimeKind.Local)),
                    EventRecurring = table.Column<int>(nullable: false),
                    AllowMaybeGoing = table.Column<bool>(nullable: false, defaultValue: true),
                    AllowNotGoing = table.Column<bool>(nullable: false, defaultValue: true),
                    OfficeId = table.Column<int>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxParticipants = table.Column<int>(maxLength: 32767, nullable: false),
                    MaxChoices = table.Column<int>(maxLength: 32767, nullable: false),
                    EventTypeId = table.Column<int>(nullable: false),
                    ResponsibleUserId = table.Column<string>(nullable: true),
                    WallId = table.Column<int>(nullable: false),
                    IsPinned = table.Column<bool>(nullable: false),
                    Offices = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Events_dbo.EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Events_dbo.Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Events_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Events_dbo.Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    Option = table.Column<string>(nullable: false),
                    Rule = table.Column<int>(nullable: false, defaultValue: 0),
                    EventId1 = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.EventOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.EventOptions_dbo.Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventOptions_Events_EventId1",
                        column: x => x.EventId1,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipantEventOptions",
                columns: table => new
                {
                    EventParticipant_Id = table.Column<int>(nullable: false),
                    EventOption_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.EventParticipantEventOptions", x => new { x.EventParticipant_Id, x.EventOption_Id });
                    table.ForeignKey(
                        name: "FK_dbo.EventParticipantEventOptions_dbo.EventOptions_EventOption_Id",
                        column: x => x.EventOption_Id,
                        principalTable: "EventOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.EventParticipantEventOptions_dbo.EventParticipants_EventParticipant_Id",
                        column: x => x.EventParticipant_Id,
                        principalTable: "EventParticipants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    LotteryId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Joined = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.LotteryParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.LotteryParticipants_dbo.Lotteries_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lotteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationsSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    EventsAppNotifications = table.Column<bool>(nullable: false),
                    EventsEmailNotifications = table.Column<bool>(nullable: false),
                    ProjectsAppNotifications = table.Column<bool>(nullable: false),
                    ProjectsEmailNotifications = table.Column<bool>(nullable: false),
                    MyPostsAppNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    MyPostsEmailNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    FollowingPostsAppNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    FollowingPostsEmailNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    EventWeeklyReminderAppNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    EventWeeklyReminderEmailNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    MentionEmailNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedLotteryEmailNotifications = table.Column<bool>(nullable: false, defaultValue: false),
                    ApplicationUser_Id = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.NotificationsSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.NotificationsSettings_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUsers",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    IsAlreadySeen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.NotificationUsers", x => new { x.NotificationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_dbo.NotificationUsers_dbo.Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Likes = table.Column<string>(nullable: true, defaultValue: "{}"),
                    MessageBody = table.Column<string>(nullable: true),
                    LastActivity = table.Column<DateTime>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    AuthorId = table.Column<string>(nullable: true),
                    Images = table.Column<string>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false),
                    SharedEventId = table.Column<string>(nullable: true),
                    WallId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Posts_dbo.Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WallMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    WallId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AppNotificationsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    EmailNotificationsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.WallMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.WallMembers_dbo.Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WallModerators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    WallId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.WallModerators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.WallModerators_dbo.Walls_WallId",
                        column: x => x.WallId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: true),
                    EndTime = table.Column<TimeSpan>(nullable: true),
                    LunchStart = table.Column<TimeSpan>(nullable: true),
                    LunchEnd = table.Column<TimeSpan>(nullable: true),
                    FullTime = table.Column<bool>(nullable: false, defaultValue: true),
                    PartTimeHours = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.WorkingHours_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    EmploymentDate = table.Column<DateTime>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    WorkingHoursId = table.Column<int>(nullable: true),
                    IsAbsent = table.Column<bool>(nullable: false),
                    IsAnonymized = table.Column<bool>(nullable: false),
                    AbsentComment = table.Column<string>(nullable: true),
                    TotalKudos = table.Column<decimal>(nullable: false),
                    RemainingKudos = table.Column<decimal>(nullable: false),
                    SittingPlacesChanged = table.Column<int>(nullable: false),
                    SpentKudos = table.Column<decimal>(nullable: false),
                    PictureId = table.Column<string>(nullable: true),
                    ManagerId = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    VacationTotalTime = table.Column<double>(nullable: true),
                    VacationUsedTime = table.Column<double>(nullable: true),
                    VacationUnusedTime = table.Column<double>(nullable: true),
                    VacationLastTimeUpdated = table.Column<DateTime>(nullable: true),
                    DailyMailingHour = table.Column<TimeSpan>(nullable: true),
                    IsManagingDirector = table.Column<bool>(nullable: false),
                    CultureCode = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    IsTutorialComplete = table.Column<bool>(nullable: false),
                    GoogleEmail = table.Column<string>(nullable: true),
                    FacebookEmail = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobPositionId = table.Column<int>(nullable: true),
                    RoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.ApplicationUser_dbo.JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ApplicationUser_dbo.Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ApplicationUser_dbo.Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_WorkingHours_WorkingHoursId",
                        column: x => x.WorkingHoursId,
                        principalTable: "WorkingHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostWatchers",
                schema: "dbo",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostWatchers", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_dbo.PostWatchers_dbo.Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.PostWatchers_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositionId",
                table: "AspNetUsers",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_RoomId",
                table: "AspNetUsers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkingHoursId",
                table: "AspNetUsers",
                column: "WorkingHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_BadgeCategoryId",
                table: "BadgeTypes",
                column: "BadgeCategoryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "BlacklistUsers",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "BlacklistUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "BookLogs",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OfficeId",
                table: "BookOffices",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "BookOffices",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "BookId_OfficeId",
                table: "BookOffices",
                columns: new[] { "BookId", "OfficeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Books",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_EventId",
                table: "EventOptions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventOptions_EventId1",
                table: "EventOptions",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventOption_Id",
                table: "EventParticipantEventOptions",
                column: "EventOption_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipant_Id",
                table: "EventParticipantEventOptions",
                column: "EventParticipant_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventId",
                table: "EventParticipants",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_EventId1",
                table: "EventParticipants",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "nci_wi_EventParticipants_CA1F6B4699FAB2347B166CEA9639C7E8",
                table: "EventParticipants",
                column: "IsDeleted")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "EventId" });

            migrationBuilder.CreateIndex(
                name: "ix_end_date",
                table: "Events",
                column: "EndDate")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_EventTypeId",
                table: "Events",
                column: "EventTypeId");

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
                name: "ix_start_date",
                table: "Events",
                column: "StartDate")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_WallId",
                table: "Events",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "EventTypes",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "ExternalLinks",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "FilterPresets",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OfficeId",
                table: "Floors",
                column: "OfficeId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Floors",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_Id",
                table: "Floors",
                column: "Picture_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "JobPositions",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Lotteries",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_LotteryId",
                table: "LotteryParticipants",
                column: "LotteryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "LotteryParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_Id",
                table: "ModuleOrganizations",
                column: "Module_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_Id",
                table: "ModuleOrganizations",
                column: "Organization_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Notifications",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Id",
                table: "NotificationsSettings",
                column: "ApplicationUser_Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "NotificationsSettings",
                column: "OrganizationId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "NotificationUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Offices",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Pages",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ParentPageId",
                table: "Pages",
                column: "ParentPageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Pictures",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LastActivity",
                table: "Posts",
                column: "LastActivity")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_WallId",
                table: "Posts",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_FloorId",
                table: "Rooms",
                column: "FloorId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Rooms",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "RoomTypes",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "SyncTokens",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "VacationPages",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "WallMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WallId",
                table: "WallMembers",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "nci_wi_WallMembers_6C8CE6B55B79BC00FDA53D9B579C2EFA",
                table: "WallMembers",
                columns: new[] { "IsDeleted", "UserId" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "WallModerators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WallId",
                table: "WallModerators",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "Walls",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserId",
                table: "WorkingHours",
                column: "ApplicationUserId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationId",
                table: "WorkingHours",
                column: "OrganizationId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_PostId",
                schema: "dbo",
                table: "PostWatchers",
                column: "PostId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                schema: "dbo",
                table: "PostWatchers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_CreatedBy",
                table: "BlacklistUsers",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_ModifiedBy",
                table: "BlacklistUsers",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BlacklistUsers_dbo.AspNetUsers_UserId",
                table: "BlacklistUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.AspNetUsers_ApplicationUserId",
                table: "BookLogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.BookLogs_dbo.BookOffices_BookOfficeId",
                table: "BookLogs",
                column: "BookOfficeId",
                principalTable: "BookOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Books_dbo.AspNetUsers_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Comments_dbo.AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Comments_dbo.Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipants_dbo.ApplicationUser_ApplicationUserId",
                table: "EventParticipants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.EventParticipants_dbo.Events_EventId",
                table: "EventParticipants",
                column: "EventId",
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
                name: "FK_dbo.Events_dbo.AspNetUsers_ResponsibleUserId",
                table: "Events",
                column: "ResponsibleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.LotteryParticipants_dbo.AspNetUsers_UserId",
                table: "LotteryParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.NotificationsSettings_dbo.AspNetUsers_ApplicationUser_Id",
                table: "NotificationsSettings",
                column: "ApplicationUser_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.NotificationUsers_dbo.AspNetUsers_UserId",
                table: "NotificationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.Posts_dbo.AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallUsers_dbo.AspNetUsers_UserId",
                table: "WallMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WallModerators_dbo.AspNetUsers_UserId",
                table: "WallModerators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.WorkingHours_dbo.AspNetUsers_ApplicationUserId",
                table: "WorkingHours",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.WorkingHours_dbo.AspNetUsers_ApplicationUserId",
                table: "WorkingHours");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BadgeTypes");

            migrationBuilder.DropTable(
                name: "BlacklistUsers");

            migrationBuilder.DropTable(
                name: "BookLogs");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EventParticipantEventOptions");

            migrationBuilder.DropTable(
                name: "ExternalLinks");

            migrationBuilder.DropTable(
                name: "FilterPresets");

            migrationBuilder.DropTable(
                name: "LotteryParticipants");

            migrationBuilder.DropTable(
                name: "ModuleOrganizations");

            migrationBuilder.DropTable(
                name: "NotificationsSettings");

            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "SyncTokens");

            migrationBuilder.DropTable(
                name: "VacationPages");

            migrationBuilder.DropTable(
                name: "WallMembers");

            migrationBuilder.DropTable(
                name: "WallModerators");

            migrationBuilder.DropTable(
                name: "PostWatchers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BadgeCategories");

            migrationBuilder.DropTable(
                name: "BookOffices");

            migrationBuilder.DropTable(
                name: "EventOptions");

            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "Lotteries");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
