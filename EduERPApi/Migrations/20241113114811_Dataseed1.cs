using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class Dataseed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FeatureRoles",
                columns: new[] { "AppRoleId", "FeatureId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("1c936d04-84ea-48d2-8840-db277e555b9e"), new Guid("adb42ecc-3032-400a-89b1-c05a685f3daa"), "Admin", 0 },
                    { new Guid("55751916-5dea-4637-974c-a5637c7e0e28"), new Guid("adb42ecc-3032-400a-89b1-c05a685f3daa"), "Student", 0 }
                });

            migrationBuilder.InsertData(
                table: "ModuleFeatures",
                columns: new[] { "FeatureId", "FeatureName", "ModuleId", "Status" },
                values: new object[] { new Guid("450ca390-13a6-4674-89a0-ab9a6b9814c2"), "Fee Management", new Guid("13a01c28-632f-4734-b9b4-a3b3c10f47ee"), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("1c936d04-84ea-48d2-8840-db277e555b9e"));

            migrationBuilder.DeleteData(
                table: "FeatureRoles",
                keyColumn: "AppRoleId",
                keyValue: new Guid("55751916-5dea-4637-974c-a5637c7e0e28"));

            migrationBuilder.DeleteData(
                table: "ModuleFeatures",
                keyColumn: "FeatureId",
                keyValue: new Guid("450ca390-13a6-4674-89a0-ab9a6b9814c2"));
        }
    }
}
