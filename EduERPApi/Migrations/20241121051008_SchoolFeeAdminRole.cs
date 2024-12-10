using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class SchoolFeeAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FeatureRoles",
                columns: new[] { "AppRoleId", "FeatureId", "RoleName", "Status" },
                values: new object[] { new Guid("50a94107-9a7b-411e-a8a2-6dfca135a5ec"), new Guid("2f82a7cd-8a0c-4043-9f20-222f554ca241"), "Admin", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("50a94107-9a7b-411e-a8a2-6dfca135a5ec"));
        }
    }
}
