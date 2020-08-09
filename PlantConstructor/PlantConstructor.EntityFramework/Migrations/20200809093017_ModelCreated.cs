using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantConstructor.EntityFramework.Migrations
{
    public partial class ModelCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "BranchAttributes",
                columns: table => new
                {
                    BranchAttributeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchAttributeName = table.Column<string>(nullable: true),
                    ProjectFKProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchAttributes", x => x.BranchAttributeID);
                    table.ForeignKey(
                        name: "FK_BranchAttributes_Projects_ProjectFKProjectID",
                        column: x => x.ProjectFKProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartAttributes",
                columns: table => new
                {
                    PartAttributeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartAttributeName = table.Column<string>(nullable: true),
                    ProjectFKProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartAttributes", x => x.PartAttributeID);
                    table.ForeignKey(
                        name: "FK_PartAttributes_Projects_ProjectFKProjectID",
                        column: x => x.ProjectFKProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PipeAttributes",
                columns: table => new
                {
                    PipeAttributeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PipeAttributeName = table.Column<string>(nullable: true),
                    ProjectFKProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipeAttributes", x => x.PipeAttributeID);
                    table.ForeignKey(
                        name: "FK_PipeAttributes_Projects_ProjectFKProjectID",
                        column: x => x.ProjectFKProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteAttributes",
                columns: table => new
                {
                    SiteAttributeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteAttributeName = table.Column<string>(nullable: true),
                    ProjectFKProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAttributes", x => x.SiteAttributeID);
                    table.ForeignKey(
                        name: "FK_SiteAttributes_Projects_ProjectFKProjectID",
                        column: x => x.ProjectFKProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(nullable: true),
                    ProjectFKProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteID);
                    table.ForeignKey(
                        name: "FK_Sites_Projects_ProjectFKProjectID",
                        column: x => x.ProjectFKProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZoneAttributes",
                columns: table => new
                {
                    ZoneAttributeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneAttributeName = table.Column<string>(nullable: true),
                    ProjectFKProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneAttributes", x => x.ZoneAttributeID);
                    table.ForeignKey(
                        name: "FK_ZoneAttributes_Projects_ProjectFKProjectID",
                        column: x => x.ProjectFKProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteAttributeValues",
                columns: table => new
                {
                    SiteAttributeValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteFKSiteID = table.Column<int>(nullable: true),
                    SiteAttributeFKSiteAttributeID = table.Column<int>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAttributeValues", x => x.SiteAttributeValueID);
                    table.ForeignKey(
                        name: "FK_SiteAttributeValues_SiteAttributes_SiteAttributeFKSiteAttributeID",
                        column: x => x.SiteAttributeFKSiteAttributeID,
                        principalTable: "SiteAttributes",
                        principalColumn: "SiteAttributeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteAttributeValues_Sites_SiteFKSiteID",
                        column: x => x.SiteFKSiteID,
                        principalTable: "Sites",
                        principalColumn: "SiteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ZoneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneName = table.Column<string>(nullable: true),
                    SiteFKSiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ZoneID);
                    table.ForeignKey(
                        name: "FK_Zones_Sites_SiteFKSiteID",
                        column: x => x.SiteFKSiteID,
                        principalTable: "Sites",
                        principalColumn: "SiteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(nullable: true),
                    ZoneFKZoneID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branches_Zones_ZoneFKZoneID",
                        column: x => x.ZoneFKZoneID,
                        principalTable: "Zones",
                        principalColumn: "ZoneID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZoneAttributeValues",
                columns: table => new
                {
                    ZoneAttributeValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneFKZoneID = table.Column<int>(nullable: true),
                    ZoneAttributeFKZoneAttributeID = table.Column<int>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneAttributeValues", x => x.ZoneAttributeValueID);
                    table.ForeignKey(
                        name: "FK_ZoneAttributeValues_ZoneAttributes_ZoneAttributeFKZoneAttributeID",
                        column: x => x.ZoneAttributeFKZoneAttributeID,
                        principalTable: "ZoneAttributes",
                        principalColumn: "ZoneAttributeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZoneAttributeValues_Zones_ZoneFKZoneID",
                        column: x => x.ZoneFKZoneID,
                        principalTable: "Zones",
                        principalColumn: "ZoneID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchAttributeValues",
                columns: table => new
                {
                    BranchAttributeValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchFKBranchID = table.Column<int>(nullable: true),
                    BranchAttributeFKBranchAttributeID = table.Column<int>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchAttributeValues", x => x.BranchAttributeValueID);
                    table.ForeignKey(
                        name: "FK_BranchAttributeValues_BranchAttributes_BranchAttributeFKBranchAttributeID",
                        column: x => x.BranchAttributeFKBranchAttributeID,
                        principalTable: "BranchAttributes",
                        principalColumn: "BranchAttributeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchAttributeValues_Branches_BranchFKBranchID",
                        column: x => x.BranchFKBranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pipes",
                columns: table => new
                {
                    PipeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PipeName = table.Column<string>(nullable: true),
                    BranchFKBranchID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipes", x => x.PipeID);
                    table.ForeignKey(
                        name: "FK_Pipes_Branches_BranchFKBranchID",
                        column: x => x.BranchFKBranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartName = table.Column<string>(nullable: true),
                    PipeFKPipeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartID);
                    table.ForeignKey(
                        name: "FK_Parts_Pipes_PipeFKPipeID",
                        column: x => x.PipeFKPipeID,
                        principalTable: "Pipes",
                        principalColumn: "PipeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PipeAttributeValues",
                columns: table => new
                {
                    PipeAttributeValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PipeFKPipeID = table.Column<int>(nullable: true),
                    SiteAttributeFKPipeAttributeID = table.Column<int>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipeAttributeValues", x => x.PipeAttributeValueID);
                    table.ForeignKey(
                        name: "FK_PipeAttributeValues_Pipes_PipeFKPipeID",
                        column: x => x.PipeFKPipeID,
                        principalTable: "Pipes",
                        principalColumn: "PipeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PipeAttributeValues_PipeAttributes_SiteAttributeFKPipeAttributeID",
                        column: x => x.SiteAttributeFKPipeAttributeID,
                        principalTable: "PipeAttributes",
                        principalColumn: "PipeAttributeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartAttributeValues",
                columns: table => new
                {
                    PartAttributeValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartFKPartID = table.Column<int>(nullable: true),
                    PartAttributeFKPartAttributeID = table.Column<int>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartAttributeValues", x => x.PartAttributeValueID);
                    table.ForeignKey(
                        name: "FK_PartAttributeValues_PartAttributes_PartAttributeFKPartAttributeID",
                        column: x => x.PartAttributeFKPartAttributeID,
                        principalTable: "PartAttributes",
                        principalColumn: "PartAttributeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartAttributeValues_Parts_PartFKPartID",
                        column: x => x.PartFKPartID,
                        principalTable: "Parts",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributes_ProjectFKProjectID",
                table: "BranchAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchAttributeFKBranchAttributeID",
                table: "BranchAttributeValues",
                column: "BranchAttributeFKBranchAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAttributeValues_BranchFKBranchID",
                table: "BranchAttributeValues",
                column: "BranchFKBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ZoneFKZoneID",
                table: "Branches",
                column: "ZoneFKZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributes_ProjectFKProjectID",
                table: "PartAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartAttributeFKPartAttributeID",
                table: "PartAttributeValues",
                column: "PartAttributeFKPartAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_PartAttributeValues_PartFKPartID",
                table: "PartAttributeValues",
                column: "PartFKPartID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PipeFKPipeID",
                table: "Parts",
                column: "PipeFKPipeID");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributes_ProjectFKProjectID",
                table: "PipeAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_PipeFKPipeID",
                table: "PipeAttributeValues",
                column: "PipeFKPipeID");

            migrationBuilder.CreateIndex(
                name: "IX_PipeAttributeValues_SiteAttributeFKPipeAttributeID",
                table: "PipeAttributeValues",
                column: "SiteAttributeFKPipeAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pipes_BranchFKBranchID",
                table: "Pipes",
                column: "BranchFKBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAttributes_ProjectFKProjectID",
                table: "SiteAttributes",
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
                name: "IX_Sites_ProjectFKProjectID",
                table: "Sites",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributes_ProjectFKProjectID",
                table: "ZoneAttributes",
                column: "ProjectFKProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneAttributeFKZoneAttributeID",
                table: "ZoneAttributeValues",
                column: "ZoneAttributeFKZoneAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAttributeValues_ZoneFKZoneID",
                table: "ZoneAttributeValues",
                column: "ZoneFKZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_SiteFKSiteID",
                table: "Zones",
                column: "SiteFKSiteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchAttributeValues");

            migrationBuilder.DropTable(
                name: "PartAttributeValues");

            migrationBuilder.DropTable(
                name: "PipeAttributeValues");

            migrationBuilder.DropTable(
                name: "SiteAttributeValues");

            migrationBuilder.DropTable(
                name: "ZoneAttributeValues");

            migrationBuilder.DropTable(
                name: "BranchAttributes");

            migrationBuilder.DropTable(
                name: "PartAttributes");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "PipeAttributes");

            migrationBuilder.DropTable(
                name: "SiteAttributes");

            migrationBuilder.DropTable(
                name: "ZoneAttributes");

            migrationBuilder.DropTable(
                name: "Pipes");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
