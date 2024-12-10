using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class ExamModuleTblCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    ExamTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.ExamTypeId);
                    table.ForeignKey(
                        name: "FK_ExamTypes_MainCourses_MainCourseId",
                        column: x => x.MainCourseId,
                        principalTable: "MainCourses",
                        principalColumn: "MainCourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QImageGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QComplexity = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QID);
                    table.ForeignKey(
                        name: "FK_Questions_SubjectTopics_TopicID",
                        column: x => x.TopicID,
                        principalTable: "SubjectTopics",
                        principalColumn: "SubTopicID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseDetialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamId);
                    table.ForeignKey(
                        name: "FK_Exams_CourseDetails_CourseDetialId",
                        column: x => x.CourseDetialId,
                        principalTable: "CourseDetails",
                        principalColumn: "CourseDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_ExamTypes_ExamTypeId",
                        column: x => x.ExamTypeId,
                        principalTable: "ExamTypes",
                        principalColumn: "ExamTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoices",
                columns: table => new
                {
                    OptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChImageGUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoices", x => x.OptId);
                    table.ForeignKey(
                        name: "FK_QuestionChoices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSchedules",
                columns: table => new
                {
                    ExamScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamOrderNo = table.Column<int>(type: "int", nullable: false),
                    ExamPaperFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreamSubjectMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSchedules", x => x.ExamScheduleId);
                    table.ForeignKey(
                        name: "FK_ExamSchedules_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSchedules_StreamSubjectMaps_StreamSubjectMapId",
                        column: x => x.StreamSubjectMapId,
                        principalTable: "StreamSubjectMaps",
                        principalColumn: "StreamSubjectMapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseDetialId",
                table: "Exams",
                column: "CourseDetialId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamTypeId",
                table: "Exams",
                column: "ExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedules_ExamId",
                table: "ExamSchedules",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedules_StreamSubjectMapId",
                table: "ExamSchedules",
                column: "StreamSubjectMapId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTypes_MainCourseId",
                table: "ExamTypes",
                column: "MainCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_QuestionId",
                table: "QuestionChoices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TopicID_QTitle",
                table: "Questions",
                columns: new[] { "TopicID", "QTitle" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSchedules");

            migrationBuilder.DropTable(
                name: "QuestionChoices");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ExamTypes");
        }
    }
}
