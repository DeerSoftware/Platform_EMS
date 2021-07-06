using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Expressions;
using EMService.Result;
using EMService.Core.Extensions;

namespace EMService
{
    /// <summary>
    /// 菜单服务接口实现
    /// </summary>
    public class MenuAppService : ApplicationService, IMenuAppService
    {

        private readonly MenuManager _menuManager;
        private readonly IRepository<Menu, Guid> _menuRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sequenceManager"></param>
        /// <param name="organizationManager"></param>
        /// <param name="organizationRepository"></param>
        public MenuAppService(
            MenuManager menuManager,
            IRepository<Menu, Guid> menuRepository
            )
        {
            _menuManager = menuManager;
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        public async Task<ServiceResult<EquipmentPowerDto>> CreateAsync(CreateMenuDto input)
        {
            EquipmentPowerDto result = new EquipmentPowerDto();
            try
            {
                var existingOrganization = await _menuRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Name));
                if (existingOrganization != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.Name);
                }
                var menu = ObjectMapper.Map<CreateMenuDto, Menu>(input);
                var data = ObjectMapper.Map<Menu, EquipmentPowerDto>(await _menuManager.CreateAsync(menu));
                return ServiceResultCode.Succeed.ServiceResultSuccess(data);
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed<EquipmentPowerDto>(ex);
            }
        }
        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            try
            {
                var menus = GetListByParentIdAsync(id, "");

                if (menus.Result.Data.Count() > 0)
                {
                    return ServiceResultCode.MenuWarning.ServiceResultWarning();
                }
                await _menuRepository.DeleteAsync(id);
                return ServiceResultCode.Succeed.ServiceResultSuccess();
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed(ex);
            }
        }
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<ServiceResult<EquipmentPowerDto>> GetAsync(Guid id)
        {
            Result<EquipmentPowerDto> result = new Result<EquipmentPowerDto>();
            try
            {
                Menu menu = await _menuRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<Menu, EquipmentPowerDto>(menu);
                return ServiceResultCode.Succeed.ServiceResultSuccess(data);
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed<EquipmentPowerDto>(ex);
            }
        }
        /// <summary>
        /// 根据上级Id查询下级数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<List<EquipmentPowerDto>>> GetListByParentIdAsync(Guid parentId, string filter)
        {
            List<EquipmentPowerDto> result = new List<EquipmentPowerDto>();
            try
            {
                List<Menu> menus = default;
                Expression<Func<Menu, bool>> where = p => p.Status == Status.Activate;
                if (string.IsNullOrEmpty(parentId.ToString()))
                {
                    where = where.And(p => p.ParentId == Guid.Empty);
                }
                else
                {
                    where = where.And(p => p.ParentId == parentId);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    where = where.And(p => p.Name.Contains(filter) || p.NickName.Contains(filter));
                }
                menus = await _menuRepository.GetListAsync(where);
                var menuList = ObjectMapper.Map<List<Menu>, List<EquipmentPowerDto>>(menus);
                var data = new List<EquipmentPowerDto>(menuList);
                return ServiceResultCode.Succeed.ServiceResultSuccess(data);
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed<List<EquipmentPowerDto>>(ex);
            }
        }
        /// <summary>
        /// 带分页查询组织对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<List<EquipmentPowerDto>>> GetListPagedAsync(Guid ParentId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null)
        {
            List<EquipmentPowerDto> equipmentPowers = new List<EquipmentPowerDto>();
            try
            {
                var foundation = await _menuRepository.GetAllPagedAsync(query =>
                {
                    if (!string.IsNullOrEmpty(ParentId.ToString()))
                    {
                        query = query.Where(p => p.ParentId == ParentId);
                    }

                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        query = query.Where(p => p.Name.Contains(filter) || p.NickName.Contains(filter));
                    }

                    return query;

                }, pageIndex, pageSize);

                foreach (var item in foundation)
                {
                    var dtos = ObjectMapper.Map<Menu, EquipmentPowerDto>(item);
                    equipmentPowers.Add(dtos);
                }
                return ServiceResultCode.Succeed.ServiceResultSuccess(equipmentPowers);
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed<List<EquipmentPowerDto>>(ex);
            }
        }
        /// <summary>
        /// 更新组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        public async Task<ServiceResult<EquipmentPowerDto>> UpdateAsync(UpdateMenuDto input)
        {
            Result<EquipmentPowerDto> result = new Result<EquipmentPowerDto>();
            try
            {
                var menu = await _menuRepository.GetAsync(input.Id);
                menu.Name = input.Name;
                menu.NickName = input.NickName;
                menu.ParentId = input.ParentId;
                menu.Sort = input.Sort;
                menu.Status = input.Status;
                menu.Url = input.Url;
                menu.Icon = input.Icon;
                var data = ObjectMapper.Map<Menu, EquipmentPowerDto>(menu);
                return ServiceResultCode.Succeed.ServiceResultSuccess(data);
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed<EquipmentPowerDto>(ex);
            }
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<List<MenuTreeDto>>> GetMenuTree()
        {
            List<MenuTreeDto> result = new List<MenuTreeDto>();
            try
            {
                var menus = await _menuRepository.GetListAsync();
                List<MenuTreeDto> menuTreeDtos = await GetMenuTreeNode(menus);
                return ServiceResultCode.Succeed.ServiceResultSuccess(menuTreeDtos);
            }
            catch (Exception ex)
            {
                return ServiceResultCode.Failed.ServiceResultFailed<List<MenuTreeDto>>(ex);
            }
        }


        #region private
        /// <summary>
        /// 组装树型结构
        /// </summary>
        /// <returns></returns>
        private async Task<List<MenuTreeDto>> GetMenuTreeNode(List<Menu> menus)
        {

            List<MenuTreeDto> fNodes = menus.Where(p => p.ParentId == Guid.Empty).Select(p => new MenuTreeDto()
            {
                Id = p.Id,
                name = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
                path = "/" + p.Url,
                meta = new MetaDto()
                {
                    icon = p.Icon,
                    title = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
                },
                component = ""
            }).ToList();

            foreach (MenuTreeDto item in fNodes)
            {
                GetTreeNodeItems(item, menus);
            }
            return fNodes;
        }
        /// <summary>
        /// 处理节点里子节点
        /// </summary>
        /// <param name="menuTreeDto"></param>
        /// <param name="menus"></param>
        private MenuTreeDto GetTreeNodeItems(MenuTreeDto menuTreeDto, List<Menu> menus)
        {
            List<MenuTreeDto> parents = menus.Where(p => p.ParentId == menuTreeDto.Id).Select(p => new MenuTreeDto()
            {
                Id = p.Id,
                name = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
                path = "/" + p.Url,
                meta = new MetaDto()
                {
                    icon = p.Icon,
                    title = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
                },
                component = p.Url
            }).ToList();

            if (parents.Count > 0)
            {
                foreach (MenuTreeDto item in parents)
                {
                    menuTreeDto.children.Add(GetTreeNodeItems(item, menus));
                }
            }
            return menuTreeDto;
        }

        /// <summary>
        /// 处理分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task NormalizeMaxResultCountAsync(PagedAndSortedResultRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(OrganizationManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.MaxResultCount > maxPageSize.Value)
            {
                input.MaxResultCount = maxPageSize.Value;
            }
        }
        #endregion
    }
}
