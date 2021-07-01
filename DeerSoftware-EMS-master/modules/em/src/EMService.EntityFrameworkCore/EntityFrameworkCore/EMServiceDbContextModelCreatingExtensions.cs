using EMService.AssetTree;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

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

            builder.Entity<Foundation>(b =>
            {
                b.ToTable("Tree_Base");
            });

            builder.Entity<DevSystem>(b =>
            {
                //Configure table & schema name
                b.ToTable(EMServiceConsts.DbTablePrefix+"Tree_System",EMServiceConsts.DbSchema);
                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.SystemGroup).IsRequired().HasMaxLength(50);
                b.Property(q => q.SystemClass).IsRequired().HasMaxLength(50);
            });

            builder.Entity<DeviceClass>(
                b =>
                {
                    b.ToTable(EMServiceConsts.DbTablePrefix+"Tree_System_Class",EMServiceConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(q=>q.ParentId).HasMaxLength(50);
                    b.Property(q=>q.Name).IsRequired().HasMaxLength(50);
                    b.Property(q => q.Code).IsRequired().HasMaxLength(50);
                });

        }
    }
}