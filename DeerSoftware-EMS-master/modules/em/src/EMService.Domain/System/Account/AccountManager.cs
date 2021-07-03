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
    /// 组织管理器
    /// </summary>
    public class AccountManager : DomainService
    {
        private readonly IRepository<Account, Guid> _dictionaryRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="dictionaryRepository"></param>
        public AccountManager(IRepository<Account, Guid> dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }
        /// <summary>
        /// 创建新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Account> CreateAsync(Account account)
        {
            try
            {
                var existingProduct = await _dictionaryRepository.FirstOrDefaultAsync(p => p.UserCode == account.UserCode);
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(account.UserCode);
                }
                return await _dictionaryRepository.InsertAsync(new Account(
                    GuidGenerator.Create(),
                    account.UserName,
                    account.RealName,
                    account.UserCode,
                    account.Mobile,
                    account.Password,
                    account.RoleId,
                    account.OrgId,
                    account.Email,
                    account.Position,
                    account.Telephone,
                    account.Gender,
                    account.Status,
                    account.Remark
                    ));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
