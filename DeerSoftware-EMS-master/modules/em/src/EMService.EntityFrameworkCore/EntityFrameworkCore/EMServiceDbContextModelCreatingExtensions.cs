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

            // 能源建模-资产基础表
            builder.Entity<Foundation>(b =>
            {
                b.ToTable("EMS_Modeling_Foundation");
                b.Ignore(b => b.ExtraProperties);
                b.Ignore(b => b.ConcurrencyStamp);
                b.Property(b => b.NodeId).ValueGeneratedOnAdd();
            });

            // 能源建模-资产设备表
            builder.Entity<Device>(b =>
            {
                b.ToTable("EMS_Modeling_Device");
            });

            // 能源建模-资产测点表
            builder.Entity<Point>(b =>
            {
                b.ToTable("EMS_Modeling_Point");
            });

            // 能源建模-资产菜单表
            builder.Entity<PopMenu>(b =>
            {
                b.ToTable("EMS_Modeling_PopMenu");
                b.Ignore(b => b.ConcurrencyStamp);
                b.Ignore(b => b.ExtraProperties);
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