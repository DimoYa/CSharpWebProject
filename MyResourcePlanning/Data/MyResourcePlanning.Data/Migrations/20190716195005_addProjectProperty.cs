using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResourcePlanning.Data.Migrations
{
    public partial class addProjectProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RequestedHours",
                table: "Projects",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedHours",
                table: "Projects");
        }
    }
}
