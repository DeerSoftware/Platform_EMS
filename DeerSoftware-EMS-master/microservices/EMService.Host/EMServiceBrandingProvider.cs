using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EMService
{
    [Dependency(ReplaceServices = true)]
    public class EMServiceBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EMService";
    }
}
