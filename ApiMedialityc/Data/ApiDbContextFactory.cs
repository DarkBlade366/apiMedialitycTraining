using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiMedialityc.Data
{
    public class ApiDbContextFactory 
        : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string []args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();

            optionsBuilder.UseNpgsql(
                configuration.GetConnectionString("DbMedialityc")
            );

            return new ApiDbContext(optionsBuilder.Options);
        }
    }
}