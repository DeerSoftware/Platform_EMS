using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace EMService.Controllers
{
    /// <summary>
    /// 组织API
    /// </summary>
    public class OrganizationController : AbpController, IOrganizationAppService
    {
        private readonly IOrganizationAppService _organizationAppService;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="organizationAppService"></param>
        public OrganizationController(IOrganizationAppService organizationAppService)
        {
            _organizationAppService = organizationAppService;
        }
        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<OrganizationDto> CreateAsync(CreateOrganizationDto input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据Id删除组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(int id)
        {
            return _organizationAppService.DeleteAsync(id);
        }
        /// <summary>
        /// 根据Id查询组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<OrganizationDto> GetAsync(int id)
        {
            return _organizationAppService.GetAsync(id);
        }
        /// <summary>
        /// 查询组织列表
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<OrganizationDto>> GetListAsync()
        {
            return _organizationAppService.GetListAsync();
        }
        /// <summary>
        /// 带分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<OrganizationDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return _organizationAppService.GetListPagedAsync(input);
        }
        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<OrganizationDto> UpdateAsync(int id, UpdateOrganizationDto input)
        {
            throw new NotImplementedException();
        }
    }
}
