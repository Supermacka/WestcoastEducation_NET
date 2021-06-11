using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class MovedRangeRestrictionToCourseDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Courses");
        }
    }
}
