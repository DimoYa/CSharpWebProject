namespace MyResourcePlanning.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DBChangesTrainingRemoveEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Training",
                table: "UserTrainings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Training",
                table: "UserTrainings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
