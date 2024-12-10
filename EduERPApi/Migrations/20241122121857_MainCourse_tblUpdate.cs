using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class MainCourse_tblUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermsInEachYear",
                table: "MainCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TermsInEachYear",
                table: "MainCourses");
        }
    }
}
