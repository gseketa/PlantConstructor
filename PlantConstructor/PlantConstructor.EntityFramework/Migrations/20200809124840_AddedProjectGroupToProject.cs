using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class AddedProjectGroupToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectGroup",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectGroup",
                table: "Projects");
        }
    }
}
