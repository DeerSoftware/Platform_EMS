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
    public class RoleManager : DomainService
    {
        private readonly IRepository<Role, Guid> _roleRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="sequenceRepository"></param>
        public RoleManager(IRepository<Role, Guid> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        /// <summary>
        /// 创建新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Role> CreateAsync(Role role)
        {
            try
            {
                var existingProduct = await _roleRepository.FirstOrDefaultAsync(p => p.Name.Equals(role.Name));
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(role.Name);
                }
                return await _roleRepository.InsertAsync(new Role(
                        GuidGenerator.Create(),
                        role.Name,
                        role.Code,
                        role.Status,
                        role.Remark
                    ));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
