using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class BranchAttributeModelEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectFKId",
                table: "BranchAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributes_ProjectFKId",
                table: "BranchAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "BranchAttributes");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "BranchAttributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributes_ProjectId",
                table: "BranchAttributes",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectId",
                table: "BranchAttributes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectId",
                table: "BranchAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributes_ProjectId",
                table: "BranchAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "BranchAttributes");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "BranchAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributes_ProjectFKId",
                table: "BranchAttributes",
                column: "ProjectFKId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectFKId",
                table: "BranchAttributes",
                column: "ProjectFKId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
