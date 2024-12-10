using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class UserOrgMap_Tbl_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrgModuleType",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserOrgMaps",
                columns: table => new
                {
                    UserOrgMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrgMaps", x => x.UserOrgMapId);
                    table.ForeignKey(
                        name: "FK_UserOrgMaps_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId");
                    table.ForeignKey(
                        name: "FK_UserOrgMaps_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrgMaps_OrgId",
                table: "UserOrgMaps",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrgMaps_UserId_OrgId",
                table: "UserOrgMaps",
                columns: new[] { "UserId", "OrgId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrgMaps");

            migrationBuilder.DropColumn(
                name: "OrgModuleType",
                table: "Organizations");
        }
    }
}
