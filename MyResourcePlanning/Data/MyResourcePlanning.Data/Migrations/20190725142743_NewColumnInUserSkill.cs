namespace MyResourcePlanning.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class NewColumnInUserSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSkills",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSkills");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Skills",
                nullable: false,
                defaultValue: 0);
        }
    }
}
