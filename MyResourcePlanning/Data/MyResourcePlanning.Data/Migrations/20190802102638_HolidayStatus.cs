using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResourcePlanning.Data.Migrations
{
    public partial class HolidayStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVacation",
                table: "UserCalendars");

            migrationBuilder.AddColumn<int>(
                name: "HolidayType",
                table: "UserCalendars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayType",
                table: "UserCalendars");

            migrationBuilder.AddColumn<bool>(
                name: "IsVacation",
                table: "UserCalendars",
                nullable: false,
                defaultValue: false);
        }
    }
}
