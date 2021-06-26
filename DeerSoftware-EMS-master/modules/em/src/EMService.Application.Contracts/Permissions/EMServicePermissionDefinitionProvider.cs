using EMService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EMService.Permissions
{
    public class EMServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EMServicePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(EMServicePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EMServiceResource>(name);
        }
    }
}
