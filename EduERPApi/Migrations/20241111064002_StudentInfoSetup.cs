using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class StudentInfoSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcdYears",
                columns: table => new
                {
                    AcdYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcdYearText = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcdYears", x => x.AcdYearId);
                });

            migrationBuilder.CreateTable(
                name: "StudentInfos",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddlDataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInfos", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentInfos_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId");
                    table.ForeignKey(
                        name: "FK_StudentInfos_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StudentYearStreamMaps",
                columns: table => new
                {
                    StudentYearStreamMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcdYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseStreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentYearStreamMaps", x => x.StudentYearStreamMapId);
                    table.ForeignKey(
                        name: "FK_StudentYearStreamMaps_AcdYears_AcdYearId",
                        column: x => x.AcdYearId,
                        principalTable: "AcdYears",
                        principalColumn: "AcdYearId");
                    table.ForeignKey(
                        name: "FK_StudentYearStreamMaps_CourseDetails_CourseStreamId",
                        column: x => x.CourseStreamId,
                        principalTable: "CourseDetails",
                        principalColumn: "CourseDetailId");
                    table.ForeignKey(
                        name: "FK_StudentYearStreamMaps_StudentInfos_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentInfos",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcdYears_AcdYearText",
                table: "AcdYears",
                column: "AcdYearText",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfos_OrgId_RegdNumber",
                table: "StudentInfos",
                columns: new[] { "OrgId", "RegdNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfos_UserId",
                table: "StudentInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentYearStreamMaps_AcdYearId",
                table: "StudentYearStreamMaps",
                column: "AcdYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentYearStreamMaps_CourseStreamId",
                table: "StudentYearStreamMaps",
                column: "CourseStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentYearStreamMaps_StudentId_AcdYearId_CourseStreamId",
                table: "StudentYearStreamMaps",
                columns: new[] { "StudentId", "AcdYearId", "CourseStreamId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentYearStreamMaps");

            migrationBuilder.DropTable(
                name: "AcdYears");

            migrationBuilder.DropTable(
                name: "StudentInfos");
        }
    }
}
