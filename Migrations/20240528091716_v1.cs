using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentJobs.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    SectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorFullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SectorDescription = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    SectorImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.SectorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserCompanyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyAddress = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    UserTelephone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    JobPostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    JobPostingTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    JobPostingDescription = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    JobPostingImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobPostingCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobPostingStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.JobPostingId);
                    table.ForeignKey(
                        name: "FK_JobPostings_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "SectorId");
                    table.ForeignKey(
                        name: "FK_JobPostings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Scorings",
                columns: table => new
                {
                    ScoringId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScoringTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ScoringDescription = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ScoringCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoringStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scorings", x => x.ScoringId);
                    table.ForeignKey(
                        name: "FK_Scorings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobPostingId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Cv = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ApplicationStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "JobPostingId");
                    table.ForeignKey(
                        name: "FK_Applications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobPostingId",
                table: "Applications",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_SectorId",
                table: "JobPostings",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_UserId",
                table: "JobPostings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scorings_UserId",
                table: "Scorings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Scorings");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
