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
        public Task<Result<MenuDto>> CreateAsync(CreateMenuDto input)
        {
            return _menuAppService.CreateAsync(input);
        }
        [HttpGet]
        public Task<Result<int>> DeleteAsync(Guid id)
        {
            return _menuAppService.DeleteAsync(id);
        }
        [HttpGet]
        public Task<Result<MenuDto>> GetAsync(Guid id)
        {
            return _menuAppService.GetAsync(id);
        }
        [HttpGet]
        public Task<Result<ListResultDto<MenuDto>>> GetListAsync()
        {
            return _menuAppService.GetListAsync();
        }
        [HttpGet]
        public Task<Result<PagedResultDto<MenuDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return _menuAppService.GetListPagedAsync(input);
        }
        [HttpGet]
        public Task<Result<MenuTreeDto>> GetMenuTree()
        {
            return _menuAppService.GetMenuTree();
        }
        [HttpPost]
        public Task<Result<MenuDto>> UpdateAsync(Guid id, UpdateMenuDto input)
        {
            return _menuAppService.UpdateAsync(id, input);
        }
    }
}
