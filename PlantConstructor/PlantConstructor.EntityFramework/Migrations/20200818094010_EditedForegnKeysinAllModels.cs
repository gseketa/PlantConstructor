using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class EditedForegnKeysinAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributeValues_Branches_BranchFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Zones_ZoneFKId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributes_Projects_ProjectFKId",
                table: "PartAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributeValues_PartAttributes_PartAttributeFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributeValues_Parts_PartFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Pipes_PipeFKId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributes_Projects_ProjectFKId",
                table: "PipeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributeValues_Pipes_PipeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributeValues_PipeAttributes_SiteAttributeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Pipes_Branches_BranchFKId",
                table: "Pipes");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributes_Projects_ProjectFKId",
                table: "SiteAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributeValues_Sites_SiteFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Sites_Projects_ProjectFKId",
                table: "Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributes_Projects_ProjectFKId",
                table: "ZoneAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributeValues_Zones_ZoneFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_Sites_SiteFKId",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Zones_SiteFKId",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributes_ProjectFKId",
                table: "ZoneAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Sites_ProjectFKId",
                table: "Sites");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteAttributeFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributes_ProjectFKId",
                table: "SiteAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Pipes_BranchFKId",
                table: "Pipes");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_PipeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_SiteAttributeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributes_ProjectFKId",
                table: "PipeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Parts_PipeFKId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartAttributeFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributes_ProjectFKId",
                table: "PartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ZoneFKId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchAttributeFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteFKId",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "ZoneAttributeFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "ZoneAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "SiteAttributeFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "SiteAttributes");

            migrationBuilder.DropColumn(
                name: "BranchFKId",
                table: "Pipes");

            migrationBuilder.DropColumn(
                name: "PipeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteAttributeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "PipeAttributes");

            migrationBuilder.DropColumn(
                name: "PipeFKId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PartAttributeFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "PartAttributes");

            migrationBuilder.DropColumn(
                name: "ZoneFKId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BranchAttributeFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchFKId",
                table: "BranchAttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "Zones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZoneAttributeId",
                table: "ZoneAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZoneId",
                table: "ZoneAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ZoneAttributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Sites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeId",
                table: "SiteAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "SiteAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "SiteAttributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Pipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PipeId",
                table: "PipeAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PiteAttributeId",
                table: "PipeAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "PipeAttributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PipeId",
                table: "Parts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartAttributeId",
                table: "PartAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "PartAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "PartAttributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZoneId",
                table: "Branches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchAttributeId",
                table: "BranchAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "BranchAttributeValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zones_SiteId",
                table: "Zones",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeId",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneId",
                table: "ZoneAttributeValues",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributes_ProjectId",
                table: "ZoneAttributes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ProjectId",
                table: "Sites",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributeValues_SiteAttributeId",
                table: "SiteAttributeValues",
                column: "SiteAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributeValues_SiteId",
                table: "SiteAttributeValues",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributes_ProjectId",
                table: "SiteAttributes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Pipes_BranchId",
                table: "Pipes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_PipeId",
                table: "PipeAttributeValues",
                column: "PipeId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_PiteAttributeId",
                table: "PipeAttributeValues",
                column: "PiteAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributes_ProjectId",
                table: "PipeAttributes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PipeId",
                table: "Parts",
                column: "PipeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartAttributeId",
                table: "PartAttributeValues",
                column: "PartAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartId",
                table: "PartAttributeValues",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributes_ProjectId",
                table: "PartAttributes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ZoneId",
                table: "Branches",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchAttributeId",
                table: "BranchAttributeValues",
                column: "BranchAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchId",
                table: "BranchAttributeValues",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeId",
                table: "BranchAttributeValues",
                column: "BranchAttributeId",
                principalTable: "BranchAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributeValues_Branches_BranchId",
                table: "BranchAttributeValues",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Zones_ZoneId",
                table: "Branches",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributes_Projects_ProjectId",
                table: "PartAttributes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributeValues_PartAttributes_PartAttributeId",
                table: "PartAttributeValues",
                column: "PartAttributeId",
                principalTable: "PartAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributeValues_Parts_PartId",
                table: "PartAttributeValues",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Pipes_PipeId",
                table: "Parts",
                column: "PipeId",
                principalTable: "Pipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributes_Projects_ProjectId",
                table: "PipeAttributes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributeValues_Pipes_PipeId",
                table: "PipeAttributeValues",
                column: "PipeId",
                principalTable: "Pipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributeValues_PipeAttributes_PiteAttributeId",
                table: "PipeAttributeValues",
                column: "PiteAttributeId",
                principalTable: "PipeAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pipes_Branches_BranchId",
                table: "Pipes",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributes_Projects_ProjectId",
                table: "SiteAttributes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeId",
                table: "SiteAttributeValues",
                column: "SiteAttributeId",
                principalTable: "SiteAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributeValues_Sites_SiteId",
                table: "SiteAttributeValues",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_Projects_ProjectId",
                table: "Sites",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributes_Projects_ProjectId",
                table: "ZoneAttributes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeId",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeId",
                principalTable: "ZoneAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributeValues_Zones_ZoneId",
                table: "ZoneAttributeValues",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_Sites_SiteId",
                table: "Zones",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeId",
                table: "BranchAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributeValues_Branches_BranchId",
                table: "BranchAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Zones_ZoneId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributes_Projects_ProjectId",
                table: "PartAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributeValues_PartAttributes_PartAttributeId",
                table: "PartAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributeValues_Parts_PartId",
                table: "PartAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Pipes_PipeId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributes_Projects_ProjectId",
                table: "PipeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributeValues_Pipes_PipeId",
                table: "PipeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributeValues_PipeAttributes_PiteAttributeId",
                table: "PipeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Pipes_Branches_BranchId",
                table: "Pipes");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributes_Projects_ProjectId",
                table: "SiteAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeId",
                table: "SiteAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributeValues_Sites_SiteId",
                table: "SiteAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Sites_Projects_ProjectId",
                table: "Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributes_Projects_ProjectId",
                table: "ZoneAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributeValues_Zones_ZoneId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_Sites_SiteId",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Zones_SiteId",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributes_ProjectId",
                table: "ZoneAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Sites_ProjectId",
                table: "Sites");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteAttributeId",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteId",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributes_ProjectId",
                table: "SiteAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Pipes_BranchId",
                table: "Pipes");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_PipeId",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_PiteAttributeId",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributes_ProjectId",
                table: "PipeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Parts_PipeId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartAttributeId",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartId",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributes_ProjectId",
                table: "PartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ZoneId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchAttributeId",
                table: "BranchAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchId",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "ZoneAttributeId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ZoneAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "SiteAttributeId",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "SiteAttributes");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Pipes");

            migrationBuilder.DropColumn(
                name: "PipeId",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "PiteAttributeId",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "PipeAttributes");

            migrationBuilder.DropColumn(
                name: "PipeId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PartAttributeId",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "PartAttributes");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BranchAttributeId",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "BranchAttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "SiteFKId",
                table: "Zones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneAttributeFKId",
                table: "ZoneAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneFKId",
                table: "ZoneAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "ZoneAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "Sites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeFKId",
                table: "SiteAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteFKId",
                table: "SiteAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "SiteAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchFKId",
                table: "Pipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PipeFKId",
                table: "PipeAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeFKId",
                table: "PipeAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "PipeAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PipeFKId",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartAttributeFKId",
                table: "PartAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartFKId",
                table: "PartAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "PartAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneFKId",
                table: "Branches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchAttributeFKId",
                table: "BranchAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchFKId",
                table: "BranchAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zones_SiteFKId",
                table: "Zones",
                column: "SiteFKId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeFKId",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneFKId",
                table: "ZoneAttributeValues",
                column: "ZoneFKId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributes_ProjectFKId",
                table: "ZoneAttributes",
                column: "ProjectFKId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ProjectFKId",
                table: "Sites",
                column: "ProjectFKId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributeValues_SiteAttributeFKId",
                table: "SiteAttributeValues",
                column: "SiteAttributeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributeValues_SiteFKId",
                table: "SiteAttributeValues",
                column: "SiteFKId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributes_ProjectFKId",
                table: "SiteAttributes",
                column: "ProjectFKId");

            migrationBuilder.CreateIndex(
                name: "IX_Pipes_BranchFKId",
                table: "Pipes",
                column: "BranchFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_PipeFKId",
                table: "PipeAttributeValues",
                column: "PipeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_SiteAttributeFKId",
                table: "PipeAttributeValues",
                column: "SiteAttributeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributes_ProjectFKId",
                table: "PipeAttributes",
                column: "ProjectFKId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PipeFKId",
                table: "Parts",
                column: "PipeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartAttributeFKId",
                table: "PartAttributeValues",
                column: "PartAttributeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartFKId",
                table: "PartAttributeValues",
                column: "PartFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributes_ProjectFKId",
                table: "PartAttributes",
                column: "ProjectFKId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ZoneFKId",
                table: "Branches",
                column: "ZoneFKId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchAttributeFKId",
                table: "BranchAttributeValues",
                column: "BranchAttributeFKId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchFKId",
                table: "BranchAttributeValues",
                column: "BranchFKId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeFKId",
                table: "BranchAttributeValues",
                column: "BranchAttributeFKId",
                principalTable: "BranchAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributeValues_Branches_BranchFKId",
                table: "BranchAttributeValues",
                column: "BranchFKId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Zones_ZoneFKId",
                table: "Branches",
                column: "ZoneFKId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributes_Projects_ProjectFKId",
                table: "PartAttributes",
                column: "ProjectFKId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributeValues_PartAttributes_PartAttributeFKId",
                table: "PartAttributeValues",
                column: "PartAttributeFKId",
                principalTable: "PartAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributeValues_Parts_PartFKId",
                table: "PartAttributeValues",
                column: "PartFKId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Pipes_PipeFKId",
                table: "Parts",
                column: "PipeFKId",
                principalTable: "Pipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributes_Projects_ProjectFKId",
                table: "PipeAttributes",
                column: "ProjectFKId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributeValues_Pipes_PipeFKId",
                table: "PipeAttributeValues",
                column: "PipeFKId",
                principalTable: "Pipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributeValues_PipeAttributes_SiteAttributeFKId",
                table: "PipeAttributeValues",
                column: "SiteAttributeFKId",
                principalTable: "PipeAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pipes_Branches_BranchFKId",
                table: "Pipes",
                column: "BranchFKId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributes_Projects_ProjectFKId",
                table: "SiteAttributes",
                column: "ProjectFKId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeFKId",
                table: "SiteAttributeValues",
                column: "SiteAttributeFKId",
                principalTable: "SiteAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributeValues_Sites_SiteFKId",
                table: "SiteAttributeValues",
                column: "SiteFKId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_Projects_ProjectFKId",
                table: "Sites",
                column: "ProjectFKId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributes_Projects_ProjectFKId",
                table: "ZoneAttributes",
                column: "ProjectFKId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeFKId",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeFKId",
                principalTable: "ZoneAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributeValues_Zones_ZoneFKId",
                table: "ZoneAttributeValues",
                column: "ZoneFKId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_Sites_SiteFKId",
                table: "Zones",
                column: "SiteFKId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
