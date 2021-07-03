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
        public async Task<Result<MenuDto>> CreateAsync(CreateMenuDto input)
        {
            Result<MenuDto> result = new Result<MenuDto>();
            try
            {
                var existingOrganization = await _menuRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Name));
                if (existingOrganization != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.Name);
                }
                var menu = ObjectMapper.Map<CreateMenuDto, Menu>(input);

                var data = ObjectMapper.Map<Menu, MenuDto>(await _menuManager.CreateAsync(menu));

                result.Code = "920001";
                result.Message = "创建成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception e)
            {
                result.Code = "920001";
                result.Message = "创建失败,失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
                //记录日志

            }
            return result;
        }
        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteAsync(Guid id)
        {
            Result<int> result = new Result<int>();
            try
            {
                await _menuRepository.DeleteAsync(id);

                result.Code = "920002";
                result.Message = "删除成功";
                result.ResultType = ResultType.Succeed;
            }
            catch (Exception)
            {
                result.Code = "920002";
                result.Message = "删除失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
                //记录日志

            }
            return result;
        }
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<MenuDto>> GetAsync(Guid id)
        {
            Result<MenuDto> result = new Result<MenuDto>();
            try
            {
                Menu menu = await _menuRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<Menu, MenuDto>(menu);

                result.Code = "920003";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "920003";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
                //记录日志

            }


            return result;
        }
        /// <summary>
        /// 查询组织对象列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<ListResultDto<MenuDto>>> GetListAsync()
        {
            Result<ListResultDto<MenuDto>> result = new Result<ListResultDto<MenuDto>>();
            try
            {
                var menus = await _menuRepository.GetListAsync();
                var menuList = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus);
                var data = new ListResultDto<MenuDto>(menuList);

                result.Code = "920004";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "920004";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        /// <summary>
        /// 带分页查询组织对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Result<PagedResultDto<MenuDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<MenuDto>> result = new Result<PagedResultDto<MenuDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                var menus = await _menuRepository
                    .OrderBy(input.Sorting ?? "Name")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _menuRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus);

                var data = new PagedResultDto<MenuDto>(totalCount, dtos);

                result.Code = "920005";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "920005";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 更新组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        public async Task<Result<MenuDto>> UpdateAsync(Guid id, UpdateMenuDto input)
        {
            Result<MenuDto> result = new Result<MenuDto>();
            try
            {
                var menu = await _menuRepository.GetAsync(id);
                menu.Name = input.Name;
                menu.NickName = input.NickName;
                menu.ParentId = input.ParentId;
                menu.Sort = input.Sort;
                menu.Status = input.Status;
                menu.Url = input.Url;
                menu.Icon = input.Icon;
                var data = ObjectMapper.Map<Menu, MenuDto>(menu);

                result.Code = "920006";
                result.Message = "更新成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "920006";
                result.Message = "更新失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<MenuTreeDto>>> GetMenuTree()
        {
            Result<List<MenuTreeDto>> result = new Result<List<MenuTreeDto>>();
            try
            {
                var menus = await _menuRepository.GetListAsync();
                List<MenuTreeDto> menuTreeDtos = await GetMenuTreeNode(menus);
                result.Code = "920007";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = menuTreeDtos;
            }
            catch (Exception)
            {
                result.Code = "920007";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
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
                component = p.Url
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
