using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class UserFeatureRoleMapTbl_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFeatureRoleMaps_FeatureRoles_FeatureRoleId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserInfos_ParentUserUserId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.DropIndex(
                name: "IX_AppUserFeatureRoleMaps_ParentUserUserId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.DropColumn(
                name: "ParentUserUserId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFeatureRoleMaps_FeatureRoles_FeatureRoleId",
                table: "AppUserFeatureRoleMaps",
                column: "FeatureRoleId",
                principalTable: "FeatureRoles",
                principalColumn: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserInfos_UserId",
                table: "AppUserFeatureRoleMaps",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFeatureRoleMaps_FeatureRoles_FeatureRoleId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserInfos_UserId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentUserUserId",
                table: "AppUserFeatureRoleMaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFeatureRoleMaps_ParentUserUserId",
                table: "AppUserFeatureRoleMaps",
                column: "ParentUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFeatureRoleMaps_FeatureRoles_FeatureRoleId",
                table: "AppUserFeatureRoleMaps",
                column: "FeatureRoleId",
                principalTable: "FeatureRoles",
                principalColumn: "AppRoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserInfos_ParentUserUserId",
                table: "AppUserFeatureRoleMaps",
                column: "ParentUserUserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
