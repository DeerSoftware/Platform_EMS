using Volo.Abp.Modularity;

namespace EMService
{
    [DependsOn(
        typeof(EMServiceApplicationModule),
        typeof(EMServiceDomainTestModule)
        )]
    public class EMServiceApplicationTestModule : AbpModule
    {

    }
}