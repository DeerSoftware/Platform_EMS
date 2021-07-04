using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMService
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IRolePowerAppService : IApplicationService
    {
        /// <summary>
        /// 带分页查询菜单对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Result<PagedResultDto<RolePowerDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        /// <summary>
        /// 查询菜单对象列表
        /// </summary>
        /// <returns></returns>
        Task<Result<ListResultDto<RolePowerDto>>> GetListAsync(string keyword);
        /// <summary>
        /// 根据Id查询菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<Result<RolePowerDto>> GetAsync(Guid id);
        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        Task<Result<RolePowerDto>> CreateAsync(CreateRolePowerDto input);
        /// <summary>
        /// 更新菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<Result<RolePowerDto>> UpdateAsync(Guid id, UpdateRolePowerDto input);
        /// <summary>
        /// 删除菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<Result<int>> DeleteAsync(Guid id);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        Task<Result<List<MenuTreeDto>>> GetMenuTree();
    }
}
