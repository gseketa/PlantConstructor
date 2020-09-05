using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class ElementNameChangeToRowIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Elements");

            migrationBuilder.AddColumn<int>(
                name: "RowIndex",
                table: "Elements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowIndex",
                table: "Elements");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Elements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
