using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMService
{
    /// <summary>
    /// 字典服务接口
    /// </summary>
    public interface IDictionaryAppService : IApplicationService
    {
        /// <summary>
        /// 带分页查询菜单对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Result<PagedResultDto<DictionaryDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        /// <summary>
        /// 查询菜单对象列表
        /// </summary>
        /// <returns></returns>
        Task<Result<ListResultDto<DictionaryDto>>> GetListAsync(string keyword);
        /// <summary>
        /// 根据Id查询菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<Result<DictionaryDto>> GetAsync(Guid id);
        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        Task<Result<DictionaryDto>> CreateAsync(CreateDictionaryDto input);
        /// <summary>
        /// 更新菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<Result<DictionaryDto>> UpdateAsync(UpdateDictionaryDto input);
        /// <summary>
        /// 删除菜单对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        Task<Result<int>> DeleteAsync(Guid id);
    }
}
