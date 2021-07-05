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
        public Task<Result<EquipmentPowerDto>> CreateAsync(CreateMenuDto input)
        {
            return _menuAppService.CreateAsync(input);
        }
        [HttpGet]
        public Task<Result<int>> DeleteAsync(Guid id)
        {
            return _menuAppService.DeleteAsync(id);
        }
        [HttpGet]
        public Task<Result<EquipmentPowerDto>> GetAsync(Guid id)
        {
            return _menuAppService.GetAsync(id);
        }
        [HttpGet]
        public Task<Result<ListResultDto<EquipmentPowerDto>>> GetListAsync(string keyword)
        {
            return _menuAppService.GetListAsync(keyword);
        }
        [HttpGet]
        public Task<Result<ListResultDto<EquipmentPowerDto>>> GetListByParentIdAsync(string parentId)
        {
            return _menuAppService.GetListByParentIdAsync(parentId);
        }

        [HttpGet]
        public Task<Result<PagedResultDto<EquipmentPowerDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return _menuAppService.GetListPagedAsync(input);
        }
        [HttpGet]
        public Task<Result<List<MenuTreeDto>>> GetMenuTree()
        {
            return _menuAppService.GetMenuTree();
        }
        [HttpPost]
        public Task<Result<EquipmentPowerDto>> UpdateAsync( UpdateMenuDto input)
        {
            return _menuAppService.UpdateAsync(input);
        }
    }
}
