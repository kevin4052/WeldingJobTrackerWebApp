using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeldingJobTrackerWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ProjectUser_JoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectLeadId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectLeadId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectLeadId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    UserMembersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectsId, x.UserMembersId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_AspNetUsers_UserMembersId",
                        column: x => x.UserMembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UserMembersId",
                table: "ProjectUser",
                column: "UserMembersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.AddColumn<string>(
                name: "ProjectLeadId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectLeadId",
                table: "Projects",
                column: "ProjectLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectLeadId",
                table: "Projects",
                column: "ProjectLeadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
