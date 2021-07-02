﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMService
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 带分页查询菜单对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MenuDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        /// <summary>
        /// 查询菜单对象列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<MenuDto>> GetListAsync();
        /// <summary>
        /// 根据Id查询菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<MenuDto> GetAsync(int id);
        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        Task<MenuDto> CreateAsync(CreateMenuDto input);
        /// <summary>
        /// 更新菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<MenuDto> UpdateAsync(int id, UpdateMenuDto input);
        /// <summary>
        /// 删除菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}