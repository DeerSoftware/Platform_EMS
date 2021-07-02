using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Settings;

namespace EMService.System.Sequence
{
    /// <summary>
    /// 序列配置管理器提供者
    /// </summary>
    public class SequenceManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(
                    OrganizationManagementSettings.MaxPageSize,
                    "100",
                    isVisibleToClients: true
                )
            );
        }
    }
}
