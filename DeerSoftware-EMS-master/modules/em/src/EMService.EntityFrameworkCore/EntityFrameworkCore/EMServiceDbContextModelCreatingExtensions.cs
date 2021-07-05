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
                b.Property(b => b.NodeId).ValueGeneratedOnAdd();
                b.HasOne(b => b.Device)
                   .WithOne(d => d.Foundation)
                   .HasForeignKey<Device>(p => p.Id);
                b.HasOne(b => b.Point)
                   .WithOne(d => d.Foundation)
                   .HasForeignKey<Point>(p => p.Id);
            });

            // 能源建模-资产设备表
            builder.Entity<Device>(b =>
            {
                b.ToTable("EMS_Modeling_Device");
                b.Ignore(b => b.ExtraProperties);
            });

            // 能源建模-资产测点表
            builder.Entity<Point>(b =>
            {
                b.ToTable("EMS_Modeling_Point");
                b.Ignore(b => b.ExtraProperties);
            });

            // 能源建模-资产菜单表
            builder.Entity<PopMenu>(b =>
            {
                b.ToTable("EMS_Modeling_PopMenu");
                b.Ignore(b => b.ExtraProperties);
            });

            builder.Entity<DeviceSystem>(b =>
            {
                //Configure table & schema name
                b.ToTable("EMS_Modeling_System");
                b.Ignore(b => b.ExtraProperties);
            });

        }
    }
}