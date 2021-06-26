using EMService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EMService.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EMServiceEntityFrameworkCoreDbMigrationsModule),
        typeof(EMServiceApplicationContractsModule)
        )]
    public class EMServiceDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
