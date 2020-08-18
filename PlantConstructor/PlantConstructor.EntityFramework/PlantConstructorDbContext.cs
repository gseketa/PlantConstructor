using Microsoft.EntityFrameworkCore;
using PlantConstructor.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.EntityFramework
{
    public class PlantConstructorDbContext : DbContext 
    {
        public DbSet<ProjectAttribute> ProjectAttributes { get; set; }
        public DbSet<AttributeG> AttributesG { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<Element> Elements { get; set; }
        
        public DbSet<Project> Projects { get; set; }

        public PlantConstructorDbContext (DbContextOptions options) : base(options) { }

    }
}
