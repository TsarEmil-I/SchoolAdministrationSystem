using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAdministrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class leftDaysFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftAbsenceDays",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeftAbsenceDays",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
