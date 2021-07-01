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
    public class OrganizationManager : DomainService
    {
        private readonly IRepository<Organization, int> _productRepository;
        private readonly IRepository<Sequence,Guid>
        public OrganizationManager(IRepository<Organization, int> productRepository)
        {
            _productRepository = productRepository;
        }
        /// <summary>
        /// 创建新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Organization> CreateAsync(Organization organization)
        {
            var existingProduct = await _productRepository.FirstOrDefaultAsync(p => p.OrgCode == organization.OrgCode);
            if (existingProduct != null)
            {
                throw new OrganizationCodeAlreadyExistsException(organization.OrgCode);
            }
            organization.Id=
            return await _productRepository.InsertAsync(organization);
        }

    }
}
