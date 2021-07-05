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

namespace EMService
{
    /// <summary>
    /// 字典分类服务接口实现
    /// </summary>
    public class DictionaryTypeAppService : ApplicationService, IDictionaryTypeAppService
    {

        private readonly DictionaryTypeManager _dictionaryTypeManager;
        private readonly IRepository<DictionaryType, Guid> _dictionaryTypeRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sequenceManager"></param>
        /// <param name="organizationManager"></param>
        /// <param name="organizationRepository"></param>
        public DictionaryTypeAppService(
            DictionaryTypeManager dictionaryTypeManager,
            IRepository<DictionaryType, Guid> dictionaryTypeRepository
            )
        {
            _dictionaryTypeManager = dictionaryTypeManager;
            _dictionaryTypeRepository = dictionaryTypeRepository;
        }

        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        public async Task<Result<DictionaryTypeDto>> CreateAsync(CreateDictionaryTypeDto input)
        {
            Result<DictionaryTypeDto> result = new Result<DictionaryTypeDto>();
            try
            {
                var existingOrganization = await _dictionaryTypeRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Name));
                if (existingOrganization != null)
                {
                    throw new DictionaryTypeCodeAlreadyExistsException(input.Name);
                }
                var menu = ObjectMapper.Map<CreateDictionaryTypeDto, DictionaryType>(input);

                var data = ObjectMapper.Map<DictionaryType, DictionaryTypeDto>(await _dictionaryTypeManager.CreateAsync(menu));

                result.Code = "920001";
                result.Message = "创建成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
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
                await _dictionaryTypeRepository.DeleteAsync(id);

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
        public async Task<Result<DictionaryTypeDto>> GetAsync(Guid id)
        {
            Result<DictionaryTypeDto> result = new Result<DictionaryTypeDto>();
            try
            {
                DictionaryType menu = await _dictionaryTypeRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<DictionaryType, DictionaryTypeDto>(menu);

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
        public async Task<Result<ListResultDto<DictionaryTypeDto>>> GetListAsync(string keyword)
        {
            Result<ListResultDto<DictionaryTypeDto>> result = new Result<ListResultDto<DictionaryTypeDto>>();
            try
            {
                List<DictionaryType> menus = default;
                Expression<Func<DictionaryType, bool>> where = p => p.IsDeleted == false;
                if (!string.IsNullOrEmpty(keyword))
                {
                    where = where.And(p => p.Name.Contains(keyword) || p.Code.Contains(keyword));
                }
                menus = await _dictionaryTypeRepository.GetListAsync(where);
                var menuList = ObjectMapper.Map<List<DictionaryType>, List<DictionaryTypeDto>>(menus);
                var data = new ListResultDto<DictionaryTypeDto>(menuList);

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
        public async Task<Result<PagedResultDto<DictionaryTypeDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<DictionaryTypeDto>> result = new Result<PagedResultDto<DictionaryTypeDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                var menus = await _dictionaryTypeRepository
                    .OrderBy(input.Sorting ?? "Name")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _dictionaryTypeRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<DictionaryType>, List<DictionaryTypeDto>>(menus);

                var data = new PagedResultDto<DictionaryTypeDto>(totalCount, dtos);

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
        public async Task<Result<DictionaryTypeDto>> UpdateAsync(UpdateDictionaryTypeDto input)
        {
            Result<DictionaryTypeDto> result = new Result<DictionaryTypeDto>();
            try
            {
                var dictionaryType = await _dictionaryTypeRepository.GetAsync(input.Id);
                dictionaryType.Name = input.Name;
                dictionaryType.Code = input.Code;
                dictionaryType.ParentId = input.ParentId;
                dictionaryType.Remark = input.Remark;
                dictionaryType.Sort = input.Sort;
                var data = ObjectMapper.Map<DictionaryType, DictionaryTypeDto>(dictionaryType);

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
        //public  Task<Result<List<MenuTreeDto>>> GetMenuTree()
        //{
        //Result<List<MenuTreeDto>> result = new Result<List<MenuTreeDto>>();
        //try
        //{
        //    var menus = await _dictionaryTypeRepository.GetListAsync();
        //    List<MenuTreeDto> menuTreeDtos = await GetMenuTreeNode(menus);
        //    result.Code = "920007";
        //    result.Message = "查询成功";
        //    result.ResultType = ResultType.Succeed;
        //    result.Data = menuTreeDtos;
        //}
        //catch (Exception)
        //{
        //    result.Code = "920007";
        //    result.Message = "查询失败，失败编码为：" + result.Code;
        //    result.ResultType = ResultType.Error;
        //}
        //return result;
        //}


        #region private
        /// <summary>
        /// 组装树型结构
        /// </summary>
        /// <returns></returns>
        //private Task<List<MenuTreeDto>> GetMenuTreeNode(List<Menu> menus)
        //{

        //    List<MenuTreeDto> fNodes = menus.Where(p => p.ParentId == Guid.Empty).Select(p => new MenuTreeDto()
        //    {
        //        Id = p.Id,
        //        name = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
        //        path = "/" + p.Url,
        //        meta = new MetaDto()
        //        {
        //            icon = p.Icon,
        //            title = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
        //        },
        //        component = p.Url
        //    }).ToList();

        //    foreach (MenuTreeDto item in fNodes)
        //    {
        //        GetTreeNodeItems(item, menus);
        //    }
        //    return fNodes;
        //}
        /// <summary>
        /// 处理节点里子节点
        /// </summary>
        /// <param name="menuTreeDto"></param>
        /// <param name="menus"></param>
        //private MenuTreeDto GetTreeNodeItems(MenuTreeDto menuTreeDto, List<Menu> menus)
        //{
        //    List<MenuTreeDto> parents = menus.Where(p => p.ParentId == menuTreeDto.Id).Select(p => new MenuTreeDto()
        //    {
        //        Id = p.Id,
        //        name = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
        //        path = "/" + p.Url,
        //        meta = new MetaDto()
        //        {
        //            icon = p.Icon,
        //            title = string.IsNullOrEmpty(p.NickName) ? p.Name : p.NickName,
        //        },
        //        component = p.Url
        //    }).ToList();

        //    if (parents.Count > 0)
        //    {
        //        foreach (MenuTreeDto item in parents)
        //        {
        //            menuTreeDto.children.Add(GetTreeNodeItems(item, menus));
        //        }
        //    }
        //    return menuTreeDto;
        //}

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
