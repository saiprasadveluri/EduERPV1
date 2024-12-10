using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class adhocmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeeMasters_CourseDetails_CourseDetailId",
                table: "FeeMasters");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseDetailId",
                table: "FeeMasters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_FeeMasters_CourseDetails_CourseDetailId",
                table: "FeeMasters",
                column: "CourseDetailId",
                principalTable: "CourseDetails",
                principalColumn: "CourseDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeeMasters_CourseDetails_CourseDetailId",
                table: "FeeMasters");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseDetailId",
                table: "FeeMasters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeeMasters_CourseDetails_CourseDetailId",
                table: "FeeMasters",
                column: "CourseDetailId",
                principalTable: "CourseDetails",
                principalColumn: "CourseDetailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
