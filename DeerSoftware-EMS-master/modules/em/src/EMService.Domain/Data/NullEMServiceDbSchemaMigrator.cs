using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EMService.Data
{
    /* This is used if database provider does't define
     * IEMServiceDbSchemaMigrator implementation.
     */
    public class NullEMServiceDbSchemaMigrator : IEMServiceDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}