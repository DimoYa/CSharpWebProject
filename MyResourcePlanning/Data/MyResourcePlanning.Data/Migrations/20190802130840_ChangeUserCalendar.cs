namespace MyResourcePlanning.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeUserCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayType",
                table: "UserCalendars");

            migrationBuilder.AddColumn<int>(
                name: "AbsenceType",
                table: "UserCalendars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceType",
                table: "UserCalendars");

            migrationBuilder.AddColumn<int>(
                name: "HolidayType",
                table: "UserCalendars",
                nullable: false,
                defaultValue: 0);
        }
    }
}
