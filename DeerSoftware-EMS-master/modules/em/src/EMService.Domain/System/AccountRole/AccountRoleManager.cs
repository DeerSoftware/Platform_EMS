using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EMService
{
    /// <summary>
    /// 账号角色管理器
    /// </summary>
    public class AccountRoleManager : DomainService
    {
        private readonly IRepository<AccountRole, Guid> _accountRoleRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="dictionaryRepository"></param>
        public AccountRoleManager(IRepository<AccountRole, Guid> accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }
        /// <summary>
        /// 创建新账号角色关系
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<AccountRole> CreateAsync(AccountRole accountRole)
        {
            try
            {
                var existingAccountRoles = await _accountRoleRepository.GetListAsync(p => p.AccountId == accountRole.AccountId);
                if (existingAccountRoles != null)
                {
                    await _accountRoleRepository.DeleteManyAsync(existingAccountRoles);
                }
                return await _accountRoleRepository.InsertAsync(new AccountRole(
                    GuidGenerator.Create(),
                    accountRole.AccountId,
                    accountRole.RoleId
                    ));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
