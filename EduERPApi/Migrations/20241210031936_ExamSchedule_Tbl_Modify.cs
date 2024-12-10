using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class ExamSchedule_Tbl_Modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamSchedules_ExamId",
                table: "ExamSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedules_ExamId_StreamSubjectMapId",
                table: "ExamSchedules",
                columns: new[] { "ExamId", "StreamSubjectMapId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamSchedules_ExamId_StreamSubjectMapId",
                table: "ExamSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedules_ExamId",
                table: "ExamSchedules",
                column: "ExamId");
        }
    }
}
