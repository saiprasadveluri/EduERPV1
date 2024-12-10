using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("76f8e76e-714a-432c-9fe8-9d02665b47a7"),
                column: "RoleName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("a71b43e4-a7f7-4e21-9ab6-2a03657bc9ec"),
                column: "RoleName",
                value: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("76f8e76e-714a-432c-9fe8-9d02665b47a7"),
                column: "RoleName",
                value: "STUDENT_MANAGEMENT Admin");

            migrationBuilder.UpdateData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("a71b43e4-a7f7-4e21-9ab6-2a03657bc9ec"),
                column: "RoleName",
                value: "STUDENT_MANAGEMENT Sudent");
        }
    }
}
