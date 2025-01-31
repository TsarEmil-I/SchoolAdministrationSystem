using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAdministrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class TeacherAddClassIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Teachers");
        }
    }
}
