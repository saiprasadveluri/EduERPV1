using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduERPApi.Migrations
{
    /// <inheritdoc />
    public partial class UserOrgMap_AdminField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsOrgAdmin",
                table: "UserOrgMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrgAdmin",
                table: "UserOrgMaps");
        }
    }
}
