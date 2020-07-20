using Microsoft.EntityFrameworkCore;
using PlantConstructor.WPF.Model;
using PlantConstructor.WPF.Model.SiteModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.EntityFramework
{
    public class PlantConstructorDbContext : DbContext 
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Site> Sites { get; set; }

        public PlantConstructorDbContext (DbContextOptions options) : base(options) { }

    }
}
