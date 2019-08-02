namespace MyResourcePlanning.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeSkillCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowedToAdd",
                table: "UserSkills");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserSkills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserSkills");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowedToAdd",
                table: "UserSkills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Skills",
                nullable: false,
                defaultValue: 0);
        }
    }
}
