using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeldingJobTrackerWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ProjectModel_createdby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerUserId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ManagerUserId",
                table: "Projects");
        }
    }
}
