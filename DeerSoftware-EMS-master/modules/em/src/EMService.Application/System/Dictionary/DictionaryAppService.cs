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
    /// 字典服务接口实现
    /// </summary>
    public class DictionaryAppService : ApplicationService, IDictionaryAppService
    {

        private readonly DictionaryManager _dictionaryManager;
        private readonly IRepository<Dictionary, Guid> _dictionaryRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sequenceManager"></param>
        /// <param name="organizationManager"></param>
        /// <param name="organizationRepository"></param>
        public DictionaryAppService(
            DictionaryManager dictionaryManager,
            IRepository<Dictionary, Guid> dictionaryRepository
            )
        {
            _dictionaryManager = dictionaryManager;
            _dictionaryRepository = dictionaryRepository;
        }

        /// <summary>
        /// 创建菜单对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        public async Task<Result<DictionaryDto>> CreateAsync(CreateDictionaryDto input)
        {
            Result<DictionaryDto> result = new Result<DictionaryDto>();
            try
            {
                var existingOrganization = await _dictionaryRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Name));
                if (existingOrganization != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.Name);
                }
                var menu = ObjectMapper.Map<CreateDictionaryDto, Dictionary>(input);

                var data = ObjectMapper.Map<Dictionary, DictionaryDto>(await _dictionaryManager.CreateAsync(menu));

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
                await _dictionaryRepository.DeleteAsync(id);

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
        public async Task<Result<DictionaryDto>> GetAsync(Guid id)
        {
            Result<DictionaryDto> result = new Result<DictionaryDto>();
            try
            {
                Dictionary menu = await _dictionaryRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<Dictionary, DictionaryDto>(menu);

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
        public async Task<Result<ListResultDto<DictionaryDto>>> GetListAsync(string keyword)
        {
            Result<ListResultDto<DictionaryDto>> result = new Result<ListResultDto<DictionaryDto>>();
            try
            {
                List<Dictionary> menus = default;
                Expression<Func<Dictionary, bool>> where = p => p.IsDeleted == false;
                if (!string.IsNullOrEmpty(keyword))
                {
                    where = where.And(p => p.Name.Contains(keyword) || p.Code.Contains(keyword));
                }
                menus = await _dictionaryRepository.GetListAsync(where);
                var menuList = ObjectMapper.Map<List<Dictionary>, List<DictionaryDto>>(menus);
                var data = new ListResultDto<DictionaryDto>(menuList);

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
        public async Task<Result<PagedResultDto<DictionaryDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<DictionaryDto>> result = new Result<PagedResultDto<DictionaryDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                var menus = await _dictionaryRepository
                    .OrderBy(input.Sorting ?? "Name")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _dictionaryRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<Dictionary>, List<DictionaryDto>>(menus);

                var data = new PagedResultDto<DictionaryDto>(totalCount, dtos);

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
        public async Task<Result<DictionaryDto>> UpdateAsync(UpdateDictionaryDto input)
        {
            Result<DictionaryDto> result = new Result<DictionaryDto>();
            try
            {
                var dictionary = await _dictionaryRepository.GetAsync(input.Id);
                dictionary.Name = input.Name;
                dictionary.Code = input.Code;
                dictionary.Sort = input.Sort;
                dictionary.DictionaryType = input.DictionaryType;

                var data = ObjectMapper.Map<Dictionary, DictionaryDto>(dictionary);

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

        #region private

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
