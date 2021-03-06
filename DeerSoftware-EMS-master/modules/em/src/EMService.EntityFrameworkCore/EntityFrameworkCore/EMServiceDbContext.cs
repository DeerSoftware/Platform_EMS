using Microsoft.EntityFrameworkCore;
using EMService.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using EMService.AssetTree;
using EMService.System.Price;

namespace EMService.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See EMServiceMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class EMServiceDbContext : AbpDbContext<EMServiceDbContext>
    {
        //public DbSet<AppUser> Users { get; set; }
        public DbSet<Foundation> Foundations { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Point> Points { get; set; }

        public DbSet<PopMenu> PopMenus { get; set; }

        public DbSet<DeviceSystem> DeviceSystems { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<DictionaryType> DictionaryTypes { get; set; }
        public DbSet<Price> Prices { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside EMServiceDbContextModelCreatingExtensions.ConfigureEMService
         */

        public EMServiceDbContext(DbContextOptions<EMServiceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

            //    b.ConfigureByConvention();
            //    b.ConfigureAbpUser();

            //    /* Configure mappings for your additional properties
            //     * Also see the EMServiceEfCoreEntityExtensionMappings class
            //     */
            //});

            /* Configure your own tables/entities inside the ConfigureEMService method */

            builder.ConfigureEMService();
        }
    }
}
