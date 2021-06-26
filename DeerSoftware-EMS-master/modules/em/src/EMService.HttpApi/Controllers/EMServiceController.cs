using EMService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EMService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class EMServiceController : AbpController
    {
        protected EMServiceController()
        {
            LocalizationResource = typeof(EMServiceResource);
        }
    }
}