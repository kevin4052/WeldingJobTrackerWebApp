using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeldingJobTrackerWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_CompanyToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companys_CompanyId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companys_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companys_CompanyId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companys_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id");
        }
    }
}
