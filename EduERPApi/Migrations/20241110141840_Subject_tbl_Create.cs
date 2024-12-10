using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class Subject_tbl_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId");
                });

            migrationBuilder.CreateTable(
                name: "StreamSubjectMaps",
                columns: table => new
                {
                    StreamSubjectMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamSubjectMaps", x => x.StreamSubjectMapId);
                    table.ForeignKey(
                        name: "FK_StreamSubjectMaps_CourseDetails_StreamId",
                        column: x => x.StreamId,
                        principalTable: "CourseDetails",
                        principalColumn: "CourseDetailId");
                    table.ForeignKey(
                        name: "FK_StreamSubjectMaps_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StreamSubjectMaps_StreamId_SubjectId",
                table: "StreamSubjectMaps",
                columns: new[] { "StreamId", "SubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StreamSubjectMaps_SubjectId",
                table: "StreamSubjectMaps",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_OrgId",
                table: "Subjects",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectName_OrgId",
                table: "Subjects",
                columns: new[] { "SubjectName", "OrgId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreamSubjectMaps");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
