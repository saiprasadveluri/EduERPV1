using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class FeeTables_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ChalanNumberSeq");

            migrationBuilder.CreateTable(
                name: "Chalans",
                columns: table => new
                {
                    ChlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChlnNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "CONVERT(VARCHAR(10), GetUTCDate(), 103)+'/'+STR(NEXT VALUE FOR ChalanNumberSeq)"),
                    MapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChlDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChalanStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chalans", x => x.ChlId);
                    table.ForeignKey(
                        name: "FK_Chalans_StudentYearStreamMaps_MapId",
                        column: x => x.MapId,
                        principalTable: "StudentYearStreamMaps",
                        principalColumn: "StudentYearStreamMapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeeHeadMasters",
                columns: table => new
                {
                    FeeHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeHeadName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FeeType = table.Column<int>(type: "int", nullable: false),
                    Terms = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeHeadMasters", x => x.FeeHeadId);
                    table.ForeignKey(
                        name: "FK_FeeHeadMasters_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId");
                });

            migrationBuilder.CreateTable(
                name: "FeeMasters",
                columns: table => new
                {
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermNo = table.Column<int>(type: "int", nullable: false),
                    CourseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MapId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AcdyearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDayNo = table.Column<int>(type: "int", nullable: false),
                    DueMonthNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeMasters", x => x.FeeId);
                    table.ForeignKey(
                        name: "FK_FeeMasters_AcdYears_AcdyearId",
                        column: x => x.AcdyearId,
                        principalTable: "AcdYears",
                        principalColumn: "AcdYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeeMasters_CourseDetails_CourseDetailId",
                        column: x => x.CourseDetailId,
                        principalTable: "CourseDetails",
                        principalColumn: "CourseDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeeMasters_FeeHeadMasters_FHeadId",
                        column: x => x.FHeadId,
                        principalTable: "FeeHeadMasters",
                        principalColumn: "FeeHeadId");
                    table.ForeignKey(
                        name: "FK_FeeMasters_StudentYearStreamMaps_MapId",
                        column: x => x.MapId,
                        principalTable: "StudentYearStreamMaps",
                        principalColumn: "StudentYearStreamMapId");
                });

            migrationBuilder.CreateTable(
                name: "ChalanLineInfo",
                columns: table => new
                {
                    ChlLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChalanLineInfo", x => x.ChlLineId);
                    table.ForeignKey(
                        name: "FK_ChalanLineInfo_Chalans_ChlId",
                        column: x => x.ChlId,
                        principalTable: "Chalans",
                        principalColumn: "ChlId");
                    table.ForeignKey(
                        name: "FK_ChalanLineInfo_FeeMasters_FeeId",
                        column: x => x.FeeId,
                        principalTable: "FeeMasters",
                        principalColumn: "FeeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChalanLineInfo_ChlId",
                table: "ChalanLineInfo",
                column: "ChlId");

            migrationBuilder.CreateIndex(
                name: "IX_ChalanLineInfo_FeeId",
                table: "ChalanLineInfo",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chalans_MapId",
                table: "Chalans",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeHeadMasters_OrgId_FeeHeadName",
                table: "FeeHeadMasters",
                columns: new[] { "OrgId", "FeeHeadName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeeMasters_AcdyearId",
                table: "FeeMasters",
                column: "AcdyearId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeMasters_CourseDetailId",
                table: "FeeMasters",
                column: "CourseDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeMasters_FHeadId",
                table: "FeeMasters",
                column: "FHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeMasters_MapId",
                table: "FeeMasters",
                column: "MapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChalanLineInfo");

            migrationBuilder.DropTable(
                name: "Chalans");

            migrationBuilder.DropTable(
                name: "FeeMasters");

            migrationBuilder.DropTable(
                name: "FeeHeadMasters");

            migrationBuilder.DropSequence(
                name: "ChalanNumberSeq");
        }
    }
}
