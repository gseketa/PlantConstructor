using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class RemovedIdAddedDomainObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectFKProjectID",
                table: "BranchAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributeValues_Branches_BranchFKBranchID",
                table: "BranchAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Zones_ZoneFKZoneID",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributes_Projects_ProjectFKProjectID",
                table: "PartAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributeValues_PartAttributes_PartAttributeFKPartAttributeID",
                table: "PartAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PartAttributeValues_Parts_PartFKPartID",
                table: "PartAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Pipes_PipeFKPipeID",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributes_Projects_ProjectFKProjectID",
                table: "PipeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributeValues_Pipes_PipeFKPipeID",
                table: "PipeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PipeAttributeValues_PipeAttributes_SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Pipes_Branches_BranchFKBranchID",
                table: "Pipes");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributes_Projects_ProjectFKProjectID",
                table: "SiteAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeFKSiteAttributeID",
                table: "SiteAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAttributeValues_Sites_SiteFKSiteID",
                table: "SiteAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Sites_Projects_ProjectFKProjectID",
                table: "Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributes_Projects_ProjectFKProjectID",
                table: "ZoneAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAttributeValues_Zones_ZoneFKZoneID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_Sites_SiteFKSiteID",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zones",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Zones_SiteFKSiteID",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneAttributeValues",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneFKZoneID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneAttributes",
                table: "ZoneAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributes_ProjectFKProjectID",
                table: "ZoneAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.DropIndex(
                name: "IX_Sites_ProjectFKProjectID",
                table: "Sites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteAttributeValues",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteAttributeFKSiteAttributeID",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteFKSiteID",
                table: "SiteAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteAttributes",
                table: "SiteAttributes");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributes_ProjectFKProjectID",
                table: "SiteAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pipes",
                table: "Pipes");

            migrationBuilder.DropIndex(
                name: "IX_Pipes_BranchFKBranchID",
                table: "Pipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PipeAttributeValues",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_PipeFKPipeID",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PipeAttributes",
                table: "PipeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributes_ProjectFKProjectID",
                table: "PipeAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_PipeFKPipeID",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartAttributeValues",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartAttributeFKPartAttributeID",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartFKPartID",
                table: "PartAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartAttributes",
                table: "PartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributes_ProjectFKProjectID",
                table: "PartAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ZoneFKZoneID",
                table: "Branches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchAttributeValues",
                table: "BranchAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchFKBranchID",
                table: "BranchAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchAttributes",
                table: "BranchAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributes_ProjectFKProjectID",
                table: "BranchAttributes");

            migrationBuilder.DropColumn(
                name: "ZoneID",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "SiteFKSiteID",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "ZoneAttributeValueID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneFKZoneID",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneAttributeID",
                table: "ZoneAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKProjectID",
                table: "ZoneAttributes");

            migrationBuilder.DropColumn(
                name: "SiteID",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "ProjectFKProjectID",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "SiteAttributeValueID",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteAttributeFKSiteAttributeID",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteFKSiteID",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteAttributeID",
                table: "SiteAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKProjectID",
                table: "SiteAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PipeID",
                table: "Pipes");

            migrationBuilder.DropColumn(
                name: "BranchFKBranchID",
                table: "Pipes");

            migrationBuilder.DropColumn(
                name: "PipeAttributeValueID",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "PipeFKPipeID",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "PipeAttributeID",
                table: "PipeAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKProjectID",
                table: "PipeAttributes");

            migrationBuilder.DropColumn(
                name: "PartID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PipeFKPipeID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PartAttributeValueID",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartAttributeFKPartAttributeID",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartFKPartID",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartAttributeID",
                table: "PartAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKProjectID",
                table: "PartAttributes");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ZoneFKZoneID",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BranchAttributeValueID",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchFKBranchID",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchAttributeID",
                table: "BranchAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKProjectID",
                table: "BranchAttributes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Zones",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SiteFKId",
                table: "Zones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ZoneAttributeValues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ZoneAttributeFKId",
                table: "ZoneAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneFKId",
                table: "ZoneAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ZoneAttributes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "ZoneAttributes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sites",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "Sites",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SiteAttributeValues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeFKId",
                table: "SiteAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteFKId",
                table: "SiteAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SiteAttributes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "SiteAttributes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Projects",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pipes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BranchFKId",
                table: "Pipes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PipeAttributeValues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PipeFKId",
                table: "PipeAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeFKId",
                table: "PipeAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PipeAttributes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "PipeAttributes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Parts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PipeFKId",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PartAttributeValues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PartAttributeFKId",
                table: "PartAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartFKId",
                table: "PartAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PartAttributes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "PartAttributes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Branches",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ZoneFKId",
                table: "Branches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BranchAttributeValues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BranchAttributeFKId",
                table: "BranchAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchFKId",
                table: "BranchAttributeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BranchAttributes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKId",
                table: "BranchAttributes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zones",
                table: "Zones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneAttributeValues",
                table: "ZoneAttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneAttributes",
                table: "ZoneAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteAttributeValues",
                table: "SiteAttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteAttributes",
                table: "SiteAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pipes",
                table: "Pipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PipeAttributeValues",
                table: "PipeAttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PipeAttributes",
                table: "PipeAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartAttributeValues",
                table: "PartAttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartAttributes",
                table: "PartAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchAttributeValues",
                table: "BranchAttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchAttributes",
                table: "BranchAttributes",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectFKId",
                table: "BranchAttributes");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zones",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Zones_SiteFKId",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneAttributeValues",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributeValues_ZoneFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneAttributes",
                table: "ZoneAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ZoneAttributes_ProjectFKId",
                table: "ZoneAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.DropIndex(
                name: "IX_Sites_ProjectFKId",
                table: "Sites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteAttributeValues",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteAttributeFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributeValues_SiteFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteAttributes",
                table: "SiteAttributes");

            migrationBuilder.DropIndex(
                name: "IX_SiteAttributes_ProjectFKId",
                table: "SiteAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pipes",
                table: "Pipes");

            migrationBuilder.DropIndex(
                name: "IX_Pipes_BranchFKId",
                table: "Pipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PipeAttributeValues",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_PipeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributeValues_SiteAttributeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PipeAttributes",
                table: "PipeAttributes");

            migrationBuilder.DropIndex(
                name: "IX_PipeAttributes_ProjectFKId",
                table: "PipeAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_PipeFKId",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartAttributeValues",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartAttributeFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributeValues_PartFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartAttributes",
                table: "PartAttributes");

            migrationBuilder.DropIndex(
                name: "IX_PartAttributes_ProjectFKId",
                table: "PartAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ZoneFKId",
                table: "Branches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchAttributeValues",
                table: "BranchAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchAttributeFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributeValues_BranchFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchAttributes",
                table: "BranchAttributes");

            migrationBuilder.DropIndex(
                name: "IX_BranchAttributes_ProjectFKId",
                table: "BranchAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "SiteFKId",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneAttributeFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "ZoneFKId",
                table: "ZoneAttributeValues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ZoneAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "ZoneAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteAttributeFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteFKId",
                table: "SiteAttributeValues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SiteAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "SiteAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pipes");

            migrationBuilder.DropColumn(
                name: "BranchFKId",
                table: "Pipes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "PipeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "SiteAttributeFKId",
                table: "PipeAttributeValues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PipeAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "PipeAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PipeFKId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartAttributeFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "PartFKId",
                table: "PartAttributeValues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PartAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "PartAttributes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ZoneFKId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchAttributeFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "BranchFKId",
                table: "BranchAttributeValues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BranchAttributes");

            migrationBuilder.DropColumn(
                name: "ProjectFKId",
                table: "BranchAttributes");

            migrationBuilder.AddColumn<int>(
                name: "ZoneID",
                table: "Zones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SiteFKSiteID",
                table: "Zones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneAttributeValueID",
                table: "ZoneAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneFKZoneID",
                table: "ZoneAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneAttributeID",
                table: "ZoneAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKProjectID",
                table: "ZoneAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteID",
                table: "Sites",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKProjectID",
                table: "Sites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeValueID",
                table: "SiteAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeFKSiteAttributeID",
                table: "SiteAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteFKSiteID",
                table: "SiteAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeID",
                table: "SiteAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKProjectID",
                table: "SiteAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PipeID",
                table: "Pipes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BranchFKBranchID",
                table: "Pipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PipeAttributeValueID",
                table: "PipeAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PipeFKPipeID",
                table: "PipeAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PipeAttributeID",
                table: "PipeAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKProjectID",
                table: "PipeAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartID",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PipeFKPipeID",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartAttributeValueID",
                table: "PartAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PartAttributeFKPartAttributeID",
                table: "PartAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartFKPartID",
                table: "PartAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartAttributeID",
                table: "PartAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKProjectID",
                table: "PartAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchID",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ZoneFKZoneID",
                table: "Branches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchAttributeValueID",
                table: "BranchAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchFKBranchID",
                table: "BranchAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchAttributeID",
                table: "BranchAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectFKProjectID",
                table: "BranchAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zones",
                table: "Zones",
                column: "ZoneID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneAttributeValues",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneAttributes",
                table: "ZoneAttributes",
                column: "ZoneAttributeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "SiteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteAttributeValues",
                table: "SiteAttributeValues",
                column: "SiteAttributeValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteAttributes",
                table: "SiteAttributes",
                column: "SiteAttributeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pipes",
                table: "Pipes",
                column: "PipeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PipeAttributeValues",
                table: "PipeAttributeValues",
                column: "PipeAttributeValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PipeAttributes",
                table: "PipeAttributes",
                column: "PipeAttributeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "PartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartAttributeValues",
                table: "PartAttributeValues",
                column: "PartAttributeValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartAttributes",
                table: "PartAttributes",
                column: "PartAttributeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "BranchID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchAttributeValues",
                table: "BranchAttributeValues",
                column: "BranchAttributeValueID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchAttributes",
                table: "BranchAttributes",
                column: "BranchAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_SiteFKSiteID",
                table: "Zones",
                column: "SiteFKSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeFKZoneAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneFKZoneID",
                table: "ZoneAttributeValues",
                column: "ZoneFKZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributes_ProjectFKProjectID",
                table: "ZoneAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ProjectFKProjectID",
                table: "Sites",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributeValues_SiteAttributeFKSiteAttributeID",
                table: "SiteAttributeValues",
                column: "SiteAttributeFKSiteAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributeValues_SiteFKSiteID",
                table: "SiteAttributeValues",
                column: "SiteFKSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributes_ProjectFKProjectID",
                table: "SiteAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Pipes_BranchFKBranchID",
                table: "Pipes",
                column: "BranchFKBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_PipeFKPipeID",
                table: "PipeAttributeValues",
                column: "PipeFKPipeID");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues",
                column: "SiteAttributeFKPipeAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributes_ProjectFKProjectID",
                table: "PipeAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PipeFKPipeID",
                table: "Parts",
                column: "PipeFKPipeID");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartAttributeFKPartAttributeID",
                table: "PartAttributeValues",
                column: "PartAttributeFKPartAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartFKPartID",
                table: "PartAttributeValues",
                column: "PartFKPartID");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributes_ProjectFKProjectID",
                table: "PartAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ZoneFKZoneID",
                table: "Branches",
                column: "ZoneFKZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues",
                column: "BranchAttributeFKBranchAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchFKBranchID",
                table: "BranchAttributeValues",
                column: "BranchFKBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributes_ProjectFKProjectID",
                table: "BranchAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributes_Projects_ProjectFKProjectID",
                table: "BranchAttributes",
                column: "ProjectFKProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues",
                column: "BranchAttributeFKBranchAttributeID",
                principalTable: "BranchAttributes",
                principalColumn: "BranchAttributeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchAttributeValues_Branches_BranchFKBranchID",
                table: "BranchAttributeValues",
                column: "BranchFKBranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Zones_ZoneFKZoneID",
                table: "Branches",
                column: "ZoneFKZoneID",
                principalTable: "Zones",
                principalColumn: "ZoneID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributes_Projects_ProjectFKProjectID",
                table: "PartAttributes",
                column: "ProjectFKProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributeValues_PartAttributes_PartAttributeFKPartAttributeID",
                table: "PartAttributeValues",
                column: "PartAttributeFKPartAttributeID",
                principalTable: "PartAttributes",
                principalColumn: "PartAttributeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartAttributeValues_Parts_PartFKPartID",
                table: "PartAttributeValues",
                column: "PartFKPartID",
                principalTable: "Parts",
                principalColumn: "PartID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Pipes_PipeFKPipeID",
                table: "Parts",
                column: "PipeFKPipeID",
                principalTable: "Pipes",
                principalColumn: "PipeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributes_Projects_ProjectFKProjectID",
                table: "PipeAttributes",
                column: "ProjectFKProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributeValues_Pipes_PipeFKPipeID",
                table: "PipeAttributeValues",
                column: "PipeFKPipeID",
                principalTable: "Pipes",
                principalColumn: "PipeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PipeAttributeValues_PipeAttributes_SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues",
                column: "SiteAttributeFKPipeAttributeID",
                principalTable: "PipeAttributes",
                principalColumn: "PipeAttributeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pipes_Branches_BranchFKBranchID",
                table: "Pipes",
                column: "BranchFKBranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributes_Projects_ProjectFKProjectID",
                table: "SiteAttributes",
                column: "ProjectFKProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeFKSiteAttributeID",
                table: "SiteAttributeValues",
                column: "SiteAttributeFKSiteAttributeID",
                principalTable: "SiteAttributes",
                principalColumn: "SiteAttributeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAttributeValues_Sites_SiteFKSiteID",
                table: "SiteAttributeValues",
                column: "SiteFKSiteID",
                principalTable: "Sites",
                principalColumn: "SiteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_Projects_ProjectFKProjectID",
                table: "Sites",
                column: "ProjectFKProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributes_Projects_ProjectFKProjectID",
                table: "ZoneAttributes",
                column: "ProjectFKProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeFKZoneAttributeID",
                principalTable: "ZoneAttributes",
                principalColumn: "ZoneAttributeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAttributeValues_Zones_ZoneFKZoneID",
                table: "ZoneAttributeValues",
                column: "ZoneFKZoneID",
                principalTable: "Zones",
                principalColumn: "ZoneID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_Sites_SiteFKSiteID",
                table: "Zones",
                column: "SiteFKSiteID",
                principalTable: "Sites",
                principalColumn: "SiteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
