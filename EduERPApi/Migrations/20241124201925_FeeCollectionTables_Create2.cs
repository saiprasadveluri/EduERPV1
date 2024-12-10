using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class FeeCollectionTables_Create2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChalanLineInfo_Chalans_ChlId",
                table: "ChalanLineInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ChalanLineInfo_FeeMasters_FeeId",
                table: "ChalanLineInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChalanLineInfo",
                table: "ChalanLineInfo");

            migrationBuilder.RenameTable(
                name: "ChalanLineInfo",
                newName: "ChalanLineInfos");

            migrationBuilder.RenameIndex(
                name: "IX_ChalanLineInfo_FeeId",
                table: "ChalanLineInfos",
                newName: "IX_ChalanLineInfos_FeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChalanLineInfo_ChlId",
                table: "ChalanLineInfos",
                newName: "IX_ChalanLineInfos_ChlId");

            migrationBuilder.CreateSequence(
                name: "EduERPSequence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChalanLineInfos",
                table: "ChalanLineInfos",
                column: "ChlLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanLineInfos_Chalans_ChlId",
                table: "ChalanLineInfos",
                column: "ChlId",
                principalTable: "Chalans",
                principalColumn: "ChlId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanLineInfos_FeeMasters_FeeId",
                table: "ChalanLineInfos",
                column: "FeeId",
                principalTable: "FeeMasters",
                principalColumn: "FeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChalanLineInfos_Chalans_ChlId",
                table: "ChalanLineInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ChalanLineInfos_FeeMasters_FeeId",
                table: "ChalanLineInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChalanLineInfos",
                table: "ChalanLineInfos");

            migrationBuilder.DropSequence(
                name: "EduERPSequence");

            migrationBuilder.RenameTable(
                name: "ChalanLineInfos",
                newName: "ChalanLineInfo");

            migrationBuilder.RenameIndex(
                name: "IX_ChalanLineInfos_FeeId",
                table: "ChalanLineInfo",
                newName: "IX_ChalanLineInfo_FeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChalanLineInfos_ChlId",
                table: "ChalanLineInfo",
                newName: "IX_ChalanLineInfo_ChlId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChalanLineInfo",
                table: "ChalanLineInfo",
                column: "ChlLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanLineInfo_Chalans_ChlId",
                table: "ChalanLineInfo",
                column: "ChlId",
                principalTable: "Chalans",
                principalColumn: "ChlId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChalanLineInfo_FeeMasters_FeeId",
                table: "ChalanLineInfo",
                column: "FeeId",
                principalTable: "FeeMasters",
                principalColumn: "FeeId");
        }
    }
}
