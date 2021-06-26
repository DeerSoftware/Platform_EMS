using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EMService.EntityFrameworkCore
{
    [DependsOn(
        typeof(EMServiceEntityFrameworkCoreModule)
        )]
    public class EMServiceEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EMServiceMigrationsDbContext>();
        }
    }
}
