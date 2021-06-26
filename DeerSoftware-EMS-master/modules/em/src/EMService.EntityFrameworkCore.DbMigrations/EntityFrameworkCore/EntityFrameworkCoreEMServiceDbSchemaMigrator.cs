using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EMService.Data;
using Volo.Abp.DependencyInjection;

namespace EMService.EntityFrameworkCore
{
    public class EntityFrameworkCoreEMServiceDbSchemaMigrator
        : IEMServiceDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreEMServiceDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the EMServiceMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<EMServiceMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}