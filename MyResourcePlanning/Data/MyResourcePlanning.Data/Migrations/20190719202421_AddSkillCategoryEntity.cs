using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResourcePlanning.Data.Migrations
{
    public partial class AddSkillCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillCategory_SkillCategoryId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillCategory",
                table: "SkillCategory");

            migrationBuilder.RenameTable(
                name: "SkillCategory",
                newName: "SkillCategories");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Trainings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Skills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillCategories",
                table: "SkillCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillCategories_SkillCategoryId",
                table: "Skills",
                column: "SkillCategoryId",
                principalTable: "SkillCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillCategories_SkillCategoryId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillCategories",
                table: "SkillCategories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "SkillCategories",
                newName: "SkillCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillCategory",
                table: "SkillCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillCategory_SkillCategoryId",
                table: "Skills",
                column: "SkillCategoryId",
                principalTable: "SkillCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
