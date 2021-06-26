using EMService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EMService
{
    [DependsOn(
        typeof(EMServiceEntityFrameworkCoreTestModule)
        )]
    public class EMServiceDomainTestModule : AbpModule
    {

    }
}