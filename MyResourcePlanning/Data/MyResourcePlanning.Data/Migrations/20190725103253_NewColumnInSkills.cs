﻿namespace MyResourcePlanning.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class NewColumnInSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Skills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");
        }
    }
}
