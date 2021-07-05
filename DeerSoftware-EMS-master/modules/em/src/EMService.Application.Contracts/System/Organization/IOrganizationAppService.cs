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
        Task<Result<PagedResultDto<OrganizationDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input, int parentId);
        /// <summary>
        /// 查询组织对象列表
        /// </summary>
        /// <returns></returns>
        Task<Result<ListResultDto<OrganizationDto>>> GetListAsync(string keyword);
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<Result<OrganizationDto>> GetAsync(int id);
        /// <summary>
        /// 创建组织对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        Task<Result<OrganizationDto>> CreateAsync(CreateOrganizationDto input);
        /// <summary>
        /// 更新组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<Result<OrganizationDto>> UpdateAsync(int id, UpdateOrganizationDto input);
        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<Result<int>> DeleteAsync(int id);
        /// <summary>
        /// 获取组织树
        /// </summary>
        /// <returns></returns>
        Task<Result<List<OrganizationTreeDto>>> GetOrganizationTree();
    }
}
