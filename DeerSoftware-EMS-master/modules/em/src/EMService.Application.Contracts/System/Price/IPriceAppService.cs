using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMService.System.Price
{
   public interface IPriceAppService : IApplicationService
    {
        /// <summary>
        /// 带条件的分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Result<PagedResultDto<PriceDto>>> GetListPagedByIdAsync(Guid BaseId,PagedAndSortedResultRequestDto input);
        /// <summary>
        /// 全展示的分页列表
        /// </summary>
        /// <returns></returns>
        Task<Result<PagedResultDto<PriceDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        /// <summary>
        /// 根据Id查询单价对象
        /// </summary>
        /// <param name="id">单价Id</param>
        /// <returns></returns>
        Task<Result<PriceDto>> GetAsync(Guid id);
        /// <summary>
        /// 创建单价对象
        /// </summary>
        /// <param name="input">单价对象</param>
        /// <returns></returns>
        Task<Result<PriceDto>> CreateAsync(CreatePriceDto input);
        /// <summary>
        /// 更新单价对象
        /// </summary>
        /// <param name="id">单价Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        Task<Result<PriceDto>> UpdateAsync(UpdatePriceDto input);
        /// <summary>
        /// 删除单价对象
        /// </summary>
        /// <param name="id">单价Id</param>
        /// <returns></returns>
        Task<Result<int>> DeleteAsync(Guid id);
    }
}
