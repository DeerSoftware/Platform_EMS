using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMService
{
    /// <summary>
    /// 组织服务接口
    /// </summary>
    public interface IOrganizationAppService : IApplicationService
    {
        /// <summary>
        /// 带分页查询组织对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrganizationDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        /// <summary>
        /// 查询组织对象列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<OrganizationDto>> GetListAsync();
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<OrganizationDto> GetAsync(int id);
        /// <summary>
        /// 创建组织对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        Task<OrganizationDto> CreateAsync(CreateOrganizationDto input);
        /// <summary>
        /// 更新组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<OrganizationDto> UpdateAsync(int id, UpdateOrganizationDto input);
        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
