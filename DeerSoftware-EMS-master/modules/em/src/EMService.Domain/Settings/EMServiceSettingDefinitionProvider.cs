using Volo.Abp.Settings;

namespace EMService.Settings
{
    public class EMServiceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EMServiceSettings.MySetting1));
        }
    }
}
