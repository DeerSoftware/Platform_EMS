using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EMService
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class EMSServiceManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public EMSServiceManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = EMServiceConsts.DbTablePrefix,
            [CanBeNull] string schema = EMServiceConsts.DbSchema)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
