using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class Subject_Tbl_modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_OrgId",
                table: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "SubjCode",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StudentLanguages",
                columns: table => new
                {
                    StudentLangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangNumber = table.Column<int>(type: "int", nullable: false),
                    StudentYearStreamMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLanguages", x => x.StudentLangId);
                    table.ForeignKey(
                        name: "FK_StudentLanguages_StreamSubjectMaps_SubjectMapId",
                        column: x => x.SubjectMapId,
                        principalTable: "StreamSubjectMaps",
                        principalColumn: "StreamSubjectMapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLanguages_StudentYearStreamMaps_StudentYearStreamMapId",
                        column: x => x.StudentYearStreamMapId,
                        principalTable: "StudentYearStreamMaps",
                        principalColumn: "StudentYearStreamMapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_OrgId_SubjCode",
                table: "Subjects",
                columns: new[] { "OrgId", "SubjCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLanguages_LangNumber_StudentYearStreamMapId",
                table: "StudentLanguages",
                columns: new[] { "LangNumber", "StudentYearStreamMapId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLanguages_LangNumber_StudentYearStreamMapId_SubjectMapId",
                table: "StudentLanguages",
                columns: new[] { "LangNumber", "StudentYearStreamMapId", "SubjectMapId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLanguages_StudentYearStreamMapId",
                table: "StudentLanguages",
                column: "StudentYearStreamMapId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLanguages_SubjectMapId",
                table: "StudentLanguages",
                column: "SubjectMapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_OrgId_SubjCode",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjCode",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_OrgId",
                table: "Subjects",
                column: "OrgId");
        }
    }
}
