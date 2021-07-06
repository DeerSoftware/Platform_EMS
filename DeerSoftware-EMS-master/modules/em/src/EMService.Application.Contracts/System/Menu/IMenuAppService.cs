using EMService.Result;
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
    public interface IMenuAppService : IApplicationService
    {
        /// <summary>
        /// 带分页查询菜单对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<List<EquipmentPowerDto>>> GetListPagedAsync(Guid parentId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null);

        /// <summary>
        /// 查询菜单下级对象对象列表
        /// </summary>
        /// <param name="parentId">上级ID</param>
        /// <returns></returns>
        Task<ServiceResult<List<EquipmentPowerDto>>> GetListByParentIdAsync(Guid parentId,string filter=null);
        /// <summary>
        /// 根据Id查询菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<ServiceResult<EquipmentPowerDto>> GetAsync(Guid id);
        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        Task<ServiceResult<EquipmentPowerDto>> CreateAsync(CreateMenuDto input);
        /// <summary>
        /// 更新菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<ServiceResult<EquipmentPowerDto>> UpdateAsync(UpdateMenuDto input);
        /// <summary>
        /// 删除菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsync(Guid id);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        Task<ServiceResult<List<MenuTreeDto>>> GetMenuTree();
    }
}
