using EMService.AssetTree;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EMService.EntityFrameworkCore
{
    public static class EMServiceDbContextModelCreatingExtensions
    {
        public static void ConfigureEMService(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(EMServiceConsts.DbTablePrefix + "YourEntities", EMServiceConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Base>(b =>
            {
                b.ToTable("Tree_Base");
            });
        }
    }
}