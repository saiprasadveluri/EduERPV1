using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class initsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationModules",
                columns: table => new
                {
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationModules", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrgAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrgId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserDetailsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ModuleFeatures",
                columns: table => new
                {
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleFeatures", x => x.FeatureId);
                    table.ForeignKey(
                        name: "FK_ModuleFeatures_ApplicationModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "ApplicationModules",
                        principalColumn: "ModuleId");
                });

            migrationBuilder.CreateTable(
                name: "MainCourses",
                columns: table => new
                {
                    MainCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSpecializationsAvailable = table.Column<int>(type: "int", maxLength: 500, nullable: false),
                    DurationInYears = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCourses", x => x.MainCourseId);
                    table.ForeignKey(
                        name: "FK_MainCourses_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureRoles",
                columns: table => new
                {
                    AppRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureRoles", x => x.AppRoleId);
                    table.ForeignKey(
                        name: "FK_FeatureRoles_ModuleFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "ModuleFeatures",
                        principalColumn: "FeatureId");
                });

            migrationBuilder.CreateTable(
                name: "OrgnizationFeatureSubscriptions",
                columns: table => new
                {
                    OrgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgnizationFeatureSubscriptions", x => new { x.FeatureId, x.OrgId });
                    table.ForeignKey(
                        name: "FK_OrgnizationFeatureSubscriptions_ModuleFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "ModuleFeatures",
                        principalColumn: "FeatureId");
                    table.ForeignKey(
                        name: "FK_OrgnizationFeatureSubscriptions_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId");
                });

            migrationBuilder.CreateTable(
                name: "CourseSpecializations",
                columns: table => new
                {
                    CourseSpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSpecializations", x => x.CourseSpecializationId);
                    table.ForeignKey(
                        name: "FK_CourseSpecializations_MainCourses_MainCourseId",
                        column: x => x.MainCourseId,
                        principalTable: "MainCourses",
                        principalColumn: "MainCourseId");
                });

            migrationBuilder.CreateTable(
                name: "AppUserFeatureRoleMaps",
                columns: table => new
                {
                    AppUserRoleMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ParentUserUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserFeatureRoleMaps", x => x.AppUserRoleMapId);
                    table.ForeignKey(
                        name: "FK_AppUserFeatureRoleMaps_FeatureRoles_FeatureRoleId",
                        column: x => x.FeatureRoleId,
                        principalTable: "FeatureRoles",
                        principalColumn: "AppRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserFeatureRoleMaps_UserInfos_ParentUserUserId",
                        column: x => x.ParentUserUserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseDetails",
                columns: table => new
                {
                    CourseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDetails", x => x.CourseDetailId);
                    table.ForeignKey(
                        name: "FK_CourseDetails_CourseSpecializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "CourseSpecializations",
                        principalColumn: "CourseSpecializationId");
                });

            migrationBuilder.InsertData(
                table: "ApplicationModules",
                columns: new[] { "ModuleId", "ModuleName", "Status" },
                values: new object[,]
                {
                    { new Guid("0942b7b7-e7e2-4964-bc09-be5cb46e2524"), "School", 1 },
                    { new Guid("13a01c28-632f-4734-b9b4-a3b3c10f47ee"), "University", 1 },
                    { new Guid("c4b4cd0e-30e2-4800-889f-a71920c481a6"), "College", 1 }
                });

            migrationBuilder.InsertData(
                table: "ModuleFeatures",
                columns: new[] { "FeatureId", "FeatureName", "ModuleId", "Status" },
                values: new object[,]
                {
                    { new Guid("15c5c626-5585-4f7d-aec2-c8986816dada"), "Fee Management", new Guid("c4b4cd0e-30e2-4800-889f-a71920c481a6"), 1 },
                    { new Guid("2f82a7cd-8a0c-4043-9f20-222f554ca241"), "Fee Management", new Guid("0942b7b7-e7e2-4964-bc09-be5cb46e2524"), 1 },
                    { new Guid("76b219bb-b72a-4125-8ec3-37aabb15c7b5"), "Student Management", new Guid("13a01c28-632f-4734-b9b4-a3b3c10f47ee"), 1 },
                    { new Guid("9e231229-8aa4-4ccb-b5c2-8960e7c62d34"), "Student Management", new Guid("0942b7b7-e7e2-4964-bc09-be5cb46e2524"), 1 },
                    { new Guid("adb42ecc-3032-400a-89b1-c05a685f3daa"), "Student Management", new Guid("c4b4cd0e-30e2-4800-889f-a71920c481a6"), 1 }
                });

            migrationBuilder.InsertData(
                table: "FeatureRoles",
                columns: new[] { "AppRoleId", "FeatureId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("76f8e76e-714a-432c-9fe8-9d02665b47a7"), new Guid("9e231229-8aa4-4ccb-b5c2-8960e7c62d34"), "STUDENT_MANAGEMENT Admin", 0 },
                    { new Guid("a71b43e4-a7f7-4e21-9ab6-2a03657bc9ec"), new Guid("9e231229-8aa4-4ccb-b5c2-8960e7c62d34"), "STUDENT_MANAGEMENT Sudent", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationModules_ModuleName",
                table: "ApplicationModules",
                column: "ModuleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFeatureRoleMaps_FeatureRoleId",
                table: "AppUserFeatureRoleMaps",
                column: "FeatureRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFeatureRoleMaps_ParentUserUserId",
                table: "AppUserFeatureRoleMaps",
                column: "ParentUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFeatureRoleMaps_UserId_FeatureRoleId",
                table: "AppUserFeatureRoleMaps",
                columns: new[] { "UserId", "FeatureRoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseDetails_SpecializationId_Year_Term",
                table: "CourseDetails",
                columns: new[] { "SpecializationId", "Year", "Term" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSpecializations_MainCourseId_SpecializationName",
                table: "CourseSpecializations",
                columns: new[] { "MainCourseId", "SpecializationName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeatureRoles_FeatureId_RoleName",
                table: "FeatureRoles",
                columns: new[] { "FeatureId", "RoleName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainCourses_OrgId_CourseName",
                table: "MainCourses",
                columns: new[] { "OrgId", "CourseName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleFeatures_ModuleId_FeatureName",
                table: "ModuleFeatures",
                columns: new[] { "ModuleId", "FeatureName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrgName",
                table: "Organizations",
                column: "OrgName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrgnizationFeatureSubscriptions_OrgId",
                table: "OrgnizationFeatureSubscriptions",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserEmail",
                table: "UserInfos",
                column: "UserEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserFeatureRoleMaps");

            migrationBuilder.DropTable(
                name: "CourseDetails");

            migrationBuilder.DropTable(
                name: "OrgnizationFeatureSubscriptions");

            migrationBuilder.DropTable(
                name: "FeatureRoles");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "CourseSpecializations");

            migrationBuilder.DropTable(
                name: "ModuleFeatures");

            migrationBuilder.DropTable(
                name: "MainCourses");

            migrationBuilder.DropTable(
                name: "ApplicationModules");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
