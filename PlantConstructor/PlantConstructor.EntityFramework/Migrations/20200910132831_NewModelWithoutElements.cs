using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class NewModelWithoutElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_AttributesG_AttributeGId",
                table: "AttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Elements_ElementId",
                table: "AttributeValues");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_AttributeGId",
                table: "AttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_ElementId",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "AttributeGId",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "AttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "ProjectAttributeID",
                table: "AttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rowindex",
                table: "AttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_ProjectAttributeID",
                table: "AttributeValues",
                column: "ProjectAttributeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_ProjectAttributes_ProjectAttributeID",
                table: "AttributeValues",
                column: "ProjectAttributeID",
                principalTable: "ProjectAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_ProjectAttributes_ProjectAttributeID",
                table: "AttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_ProjectAttributeID",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectAttributeID",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "Rowindex",
                table: "AttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "AttributeGId",
                table: "AttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "AttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    RowIndex = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_AttributeGId",
                table: "AttributeValues",
                column: "AttributeGId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_ElementId",
                table: "AttributeValues",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ProjectId",
                table: "Elements",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_AttributesG_AttributeGId",
                table: "AttributeValues",
                column: "AttributeGId",
                principalTable: "AttributesG",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Elements_ElementId",
                table: "AttributeValues",
                column: "ElementId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
