using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class ExamTbl_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AcdYearId",
                table: "Exams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StudentExamScheduleMaps",
                columns: table => new
                {
                    StudentExamScheduleMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentYearStreamMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamScheduleMaps", x => x.StudentExamScheduleMapId);
                    table.ForeignKey(
                        name: "FK_StudentExamScheduleMaps_ExamSchedules_ExamScheduleId",
                        column: x => x.ExamScheduleId,
                        principalTable: "ExamSchedules",
                        principalColumn: "ExamScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExamScheduleMaps_StudentYearStreamMaps_StudentYearStreamMapId",
                        column: x => x.StudentYearStreamMapId,
                        principalTable: "StudentYearStreamMaps",
                        principalColumn: "StudentYearStreamMapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_AcdYearId",
                table: "Exams",
                column: "AcdYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamScheduleMaps_ExamScheduleId",
                table: "StudentExamScheduleMaps",
                column: "ExamScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamScheduleMaps_StudentYearStreamMapId",
                table: "StudentExamScheduleMaps",
                column: "StudentYearStreamMapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AcdYears_AcdYearId",
                table: "Exams",
                column: "AcdYearId",
                principalTable: "AcdYears",
                principalColumn: "AcdYearId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AcdYears_AcdYearId",
                table: "Exams");

            migrationBuilder.DropTable(
                name: "StudentExamScheduleMaps");

            migrationBuilder.DropIndex(
                name: "IX_Exams_AcdYearId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AcdYearId",
                table: "Exams");
        }
    }
}
