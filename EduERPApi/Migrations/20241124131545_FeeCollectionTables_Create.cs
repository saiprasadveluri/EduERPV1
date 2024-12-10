using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class FeeCollectionTables_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChlnNumber",
                table: "Chalans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "CONVERT(VARCHAR(10), GetUTCDate(), 103)+'/'+STR(NEXT VALUE FOR ChalanNumberSeq)");

            migrationBuilder.AddColumn<int>(
                name: "DueMon",
                table: "ChalanLineInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FeeHeadName",
                table: "ChalanLineInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PaidAmt",
                table: "ChalanLineInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TermNo",
                table: "ChalanLineInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FeeCollections",
                columns: table => new
                {
                    FeeColId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChlnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayType = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColDate = table.Column<DateTime>(type: "date", nullable: false),
                    StudentYearStreamMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeCollections", x => x.FeeColId);
                    table.ForeignKey(
                        name: "FK_FeeCollections_Chalans_ChlnId",
                        column: x => x.ChlnId,
                        principalTable: "Chalans",
                        principalColumn: "ChlId");
                    table.ForeignKey(
                        name: "FK_FeeCollections_StudentYearStreamMaps_StudentYearStreamMapId",
                        column: x => x.StudentYearStreamMapId,
                        principalTable: "StudentYearStreamMaps",
                        principalColumn: "StudentYearStreamMapId");
                });

            migrationBuilder.CreateTable(
                name: "FeeConcessions",
                columns: table => new
                {
                    ConId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ConcessionType = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeConcessions", x => x.ConId);
                    table.ForeignKey(
                        name: "FK_FeeConcessions_FeeMasters_FeeId",
                        column: x => x.FeeId,
                        principalTable: "FeeMasters",
                        principalColumn: "FeeId");
                    table.ForeignKey(
                        name: "FK_FeeConcessions_StudentYearStreamMaps_MapId",
                        column: x => x.MapId,
                        principalTable: "StudentYearStreamMaps",
                        principalColumn: "StudentYearStreamMapId");
                });

            migrationBuilder.CreateTable(
                name: "FeeCollectionLineItems",
                columns: table => new
                {
                    LineItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeCollectionLineItems", x => x.LineItemId);
                    table.ForeignKey(
                        name: "FK_FeeCollectionLineItems_FeeCollections_ColId",
                        column: x => x.ColId,
                        principalTable: "FeeCollections",
                        principalColumn: "FeeColId");
                    table.ForeignKey(
                        name: "FK_FeeCollectionLineItems_FeeMasters_FeeId",
                        column: x => x.FeeId,
                        principalTable: "FeeMasters",
                        principalColumn: "FeeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeeCollectionLineItems_ColId",
                table: "FeeCollectionLineItems",
                column: "ColId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeCollectionLineItems_FeeId",
                table: "FeeCollectionLineItems",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeCollections_ChlnId",
                table: "FeeCollections",
                column: "ChlnId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeCollections_StudentYearStreamMapId",
                table: "FeeCollections",
                column: "StudentYearStreamMapId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeConcessions_FeeId",
                table: "FeeConcessions",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeConcessions_MapId",
                table: "FeeConcessions",
                column: "MapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeeCollectionLineItems");

            migrationBuilder.DropTable(
                name: "FeeConcessions");

            migrationBuilder.DropTable(
                name: "FeeCollections");

            migrationBuilder.DropColumn(
                name: "DueMon",
                table: "ChalanLineInfo");

            migrationBuilder.DropColumn(
                name: "FeeHeadName",
                table: "ChalanLineInfo");

            migrationBuilder.DropColumn(
                name: "PaidAmt",
                table: "ChalanLineInfo");

            migrationBuilder.DropColumn(
                name: "TermNo",
                table: "ChalanLineInfo");

            migrationBuilder.AlterColumn<string>(
                name: "ChlnNumber",
                table: "Chalans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR(10), GetUTCDate(), 103)+'/'+STR(NEXT VALUE FOR ChalanNumberSeq)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
