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
            options.UseSqlServer("Server=innsql01; Database=PlantConstructorDB3;User ID=gse_admin;Password=Gs48800609;MultipleActiveResultSets=False;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }
            );

            return new PlantConstructorDbContext(options.Options);
        }
    }
}
