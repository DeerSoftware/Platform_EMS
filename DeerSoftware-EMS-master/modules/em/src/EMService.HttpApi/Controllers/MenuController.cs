using EMService.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace EMService.Controllers
{
    /// <summary>
    /// 组织API
    /// </summary>
    [RemoteService]
    [Route("api/[controller]/[action]")]
    public class MenuController : AbpController, IMenuAppService
    {
        private readonly IMenuAppService _menuAppService;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="organizationAppService"></param>
        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }
        [HttpPost]
        public Task<ServiceResult<EquipmentPowerDto>> CreateAsync(CreateMenuDto input)
        {
            return _menuAppService.CreateAsync(input);
        }
        [HttpGet]
        public Task<ServiceResult> DeleteAsync(Guid id)
        {
            return _menuAppService.DeleteAsync(id);
        }
        [HttpGet]
        public Task<ServiceResult<EquipmentPowerDto>> GetAsync(Guid id)
        {
            return _menuAppService.GetAsync(id);
        }
        [HttpGet]
        public Task<ServiceResult<List<EquipmentPowerDto>>> GetListByParentIdAsync(Guid parentId,string Filers)
        {
            return _menuAppService.GetListByParentIdAsync(parentId, Filers);
        }

        [HttpGet]
        public Task<ServiceResult<List<EquipmentPowerDto>>> GetListPagedAsync(Guid ParentId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null)
        {
            return _menuAppService.GetListPagedAsync(ParentId, pageIndex,pageSize,filter);
        }
        [HttpGet]
        public Task<ServiceResult<List<MenuTreeDto>>> GetMenuTree()
        {
            return _menuAppService.GetMenuTree();
        }
        [HttpPost]
        public Task<ServiceResult<EquipmentPowerDto>> UpdateAsync( UpdateMenuDto input)
        {
            return _menuAppService.UpdateAsync(input);
        }
    }
}
