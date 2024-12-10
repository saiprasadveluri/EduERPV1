using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class User_FeatureRole_Adjstments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserInfos_UserId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppUserFeatureRoleMaps",
                newName: "UserOrgMapId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFeatureRoleMaps_UserId_FeatureRoleId",
                table: "AppUserFeatureRoleMaps",
                newName: "IX_AppUserFeatureRoleMaps_UserOrgMapId_FeatureRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserOrgMaps_UserOrgMapId",
                table: "AppUserFeatureRoleMaps",
                column: "UserOrgMapId",
                principalTable: "UserOrgMaps",
                principalColumn: "UserOrgMapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserOrgMaps_UserOrgMapId",
                table: "AppUserFeatureRoleMaps");

            migrationBuilder.RenameColumn(
                name: "UserOrgMapId",
                table: "AppUserFeatureRoleMaps",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFeatureRoleMaps_UserOrgMapId_FeatureRoleId",
                table: "AppUserFeatureRoleMaps",
                newName: "IX_AppUserFeatureRoleMaps_UserId_FeatureRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFeatureRoleMaps_UserInfos_UserId",
                table: "AppUserFeatureRoleMaps",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserId");
        }
    }
}
