using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EMService.Controllers
{
    [RemoteService]
    [Area("DevSystem")]
    [Route("api/DevSystem")]
    public class DevSystemController : AbpController, IDevSystemAppService
    {
        private readonly IDevSystemAppService _devSystemAppService;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="organizationAppService"></param>
        public DevSystemController(IDevSystemAppService devSystemAppService)
        {
            _devSystemAppService = devSystemAppService;
        }
        [HttpPost]
        [Route("AddOrUpdateAssetNode")]
        public Task AddOrUpdateAssetNode(dynamic assetData)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("DelAssetNode")]
        public Task DelAssetNode(Guid idKey)
        {
            return _devSystemAppService.DelAssetNode(idKey);
        }
        [HttpPost]
        [Route("GetAssetDataById")]
        public Task<dynamic> getAssetDataById(int deviceType, Guid idKey)
        {
            return _devSystemAppService.getAssetDataById(deviceType,idKey);
        }
        [HttpPost]
        [Route("GetAssetDataByParentId")]
        public Task<List<dynamic>> getAssetDataByParentId(Guid idKey)
        {
            return _devSystemAppService.getAssetDataByParentId(idKey);
        }
    }
}
