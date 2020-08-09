using Microsoft.EntityFrameworkCore;
using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Model.BranchModel;
using PlantConstructor.Domain.Model.PartModel;
using PlantConstructor.Domain.Model.PipeModel;
using PlantConstructor.Domain.Model.SiteModel;
using PlantConstructor.Domain.Model.ZoneModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.EntityFramework
{
    public class PlantConstructorDbContext : DbContext 
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchAttribute> BranchAttributes { get; set; }
        public DbSet<BranchAttributeValue> BranchAttributeValues { get; set; }

        public DbSet<Part> Parts { get; set; }
        public DbSet<PartAttribute> PartAttributes { get; set; }
        public DbSet<PartAttributeValue> PartAttributeValues { get; set; }

        public DbSet<Pipe> Pipes { get; set; }
        public DbSet<PipeAttribute> PipeAttributes { get; set; }
        public DbSet<PipeAttributeValue> PipeAttributeValues { get; set; }

        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteAttribute> SiteAttributes { get; set; }
        public DbSet<SiteAttributeValue> SiteAttributeValues { get; set; }

        public DbSet<Zone> Zones { get; set; }
        public DbSet<ZoneAttribute> ZoneAttributes { get; set; }
        public DbSet<ZoneAttributeValue> ZoneAttributeValues { get; set; }

        public DbSet<Project> Projects { get; set; }

        public PlantConstructorDbContext (DbContextOptions options) : base(options) { }

    }
}
