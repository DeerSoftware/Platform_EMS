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
    /// 账号服务接口实现
    /// </summary>
    public class AccountAppService : ApplicationService, IAccountAppService
    {

        private readonly AccountManager _accountManager;
        private readonly IRepository<Account, Guid> _accountRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="accountManager"></param>
        /// <param name="accountRepository"></param>
        public AccountAppService(
            AccountManager accountManager,
            IRepository<Account, Guid> accountRepository
            )
        {
            _accountManager = accountManager;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// 创建账号对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        public async Task<Result<AccountDto>> CreateAsync(CreateAccountDto input)
        {
            Result<AccountDto> result = new Result<AccountDto>();
            try
            {
                var existingOrganization = await _accountRepository.FirstOrDefaultAsync(p => p.UserName.Equals(input.UserName));
                if (existingOrganization != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.UserName);
                }
                var account = ObjectMapper.Map<CreateAccountDto, Account>(input);

                var data = ObjectMapper.Map<Account, AccountDto>(await _accountManager.CreateAsync(account));

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
        /// 删除账号对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteAsync(Guid id)
        {
            Result<int> result = new Result<int>();
            try
            {
                await _accountRepository.DeleteAsync(id);

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
        /// 根据Id查询账号对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<AccountDto>> GetAsync(Guid id)
        {
            Result<AccountDto> result = new Result<AccountDto>();
            try
            {
                Account account = await _accountRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<Account, AccountDto>(account);

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
        /// 查询账号对象列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<ListResultDto<AccountDto>>> GetListAsync(string keyword)
        {
            Result<ListResultDto<AccountDto>> result = new Result<ListResultDto<AccountDto>>();
            try
            {
                List<Account> accounts = default;
                Expression<Func<Account, bool>> where = p => p.IsDeleted == false;
                if (!string.IsNullOrEmpty(keyword))
                {
                    where = where.And(p => p.UserName.Contains(keyword) || p.UserCode.Contains(keyword));
                }
                accounts = await _accountRepository.GetListAsync(where);
                var accountList = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);
                var data = new ListResultDto<AccountDto>(accountList);

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
        /// 带分页查询账号对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Result<PagedResultDto<AccountDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<AccountDto>> result = new Result<PagedResultDto<AccountDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                var accounts = await _accountRepository
                    .OrderBy(input.Sorting ?? "Name")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _accountRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<Account>, List<AccountDto>>(accounts);

                var data = new PagedResultDto<AccountDto>(totalCount, dtos);

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
        /// 更新账号对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        public async Task<Result<AccountDto>> UpdateAsync(UpdateAccountDto input)
        {
            Result<AccountDto> result = new Result<AccountDto>();
            try
            {
                var accounts = await _accountRepository.FirstOrDefaultAsync(p=>p.Id==input.Id);
               

                var data = ObjectMapper.Map<Account, AccountDto>(accounts);

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
