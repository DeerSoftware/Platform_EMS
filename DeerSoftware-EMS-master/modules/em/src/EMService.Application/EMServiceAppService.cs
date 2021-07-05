using System;
using System.Collections.Generic;
using System.Text;
using EMService.Localization;
using Volo.Abp.Application.Services;

namespace EMService
{
    /* Inherit your application services from this class.
     */
    public abstract class EMServiceAppService : ApplicationService
    {
        protected EMServiceAppService()
        {
            LocalizationResource = typeof(EMServiceResource);
        }

        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            //use AutoMapper for mapping objects
            return ObjectMapper.Map<TSource, TDestination>(source);
        }
    }
}
