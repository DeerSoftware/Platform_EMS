using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EMService.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class EMServiceMigrationsDbContextFactory : IDesignTimeDbContextFactory<EMServiceMigrationsDbContext>
    {
        public EMServiceMigrationsDbContext CreateDbContext(string[] args)
        {
            EMServiceEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EMServiceMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new EMServiceMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EMService.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
