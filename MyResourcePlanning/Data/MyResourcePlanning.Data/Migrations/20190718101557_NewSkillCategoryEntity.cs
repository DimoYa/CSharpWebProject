namespace MyResourcePlanning.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class NewSkillCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkillCategoryId",
                table: "Skills",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SkillCategory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillCategoryId",
                table: "Skills",
                column: "SkillCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillCategory_SkillCategoryId",
                table: "Skills",
                column: "SkillCategoryId",
                principalTable: "SkillCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillCategory_SkillCategoryId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "SkillCategory");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillCategoryId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillCategoryId",
                table: "Skills");
        }
    }
}
