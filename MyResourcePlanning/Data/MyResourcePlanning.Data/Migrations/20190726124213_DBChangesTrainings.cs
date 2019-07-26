namespace MyResourcePlanning.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DBChangesTrainings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSkills");

            migrationBuilder.AddColumn<int>(
                name: "Training",
                table: "UserTrainings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Trainings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Training",
                table: "UserTrainings");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Trainings");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSkills",
                nullable: false,
                defaultValue: false);
        }
    }
}
