using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class ExamType_tblModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamTypes_MainCourseId",
                table: "ExamTypes");

            migrationBuilder.AlterColumn<string>(
                name: "ExamTypeName",
                table: "ExamTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTypes_MainCourseId_ExamTypeName",
                table: "ExamTypes",
                columns: new[] { "MainCourseId", "ExamTypeName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamTypes_MainCourseId_ExamTypeName",
                table: "ExamTypes");

            migrationBuilder.AlterColumn<string>(
                name: "ExamTypeName",
                table: "ExamTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTypes_MainCourseId",
                table: "ExamTypes",
                column: "MainCourseId");
        }
    }
}
