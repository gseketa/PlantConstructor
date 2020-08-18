using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.EntityFramework
{
    public class PlantConstructorDbContextFactory : IDesignTimeDbContextFactory<PlantConstructorDbContext>
    {
        public PlantConstructorDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<PlantConstructorDbContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=PlantConstructorDB2; trusted_Connection=True");

            return new PlantConstructorDbContext(options.Options);
        }
    }
}
