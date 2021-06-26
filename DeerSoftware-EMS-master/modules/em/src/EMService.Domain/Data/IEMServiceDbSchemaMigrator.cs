using System.Threading.Tasks;

namespace EMService.Data
{
    public interface IEMServiceDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
