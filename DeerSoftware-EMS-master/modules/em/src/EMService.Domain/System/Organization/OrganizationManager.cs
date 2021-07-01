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

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="sequenceRepository"></param>
        public OrganizationManager(IRepository<Organization, int> organizationRepository)
        {
            _productRepository = organizationRepository;
        }
        /// <summary>
        /// 创建新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Organization> CreateAsync(Organization organization, int id)
        {
            try
            {
                var existingProduct = await _productRepository.FirstOrDefaultAsync(p => p.OrgCode == organization.OrgCode);
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(organization.OrgCode);
                }
                return await _productRepository.InsertAsync(new Organization(
                    id,
                    organization.OrgName,
                    organization.OrgNickName,
                    organization.ParentId,
                    organization.OrgCode,
                    organization.Phone,
                    organization.PhoneExt,
                    organization.Email,
                    organization.Sort,
                    organization.ResponsiblePerson,
                    organization.Address,
                    organization.Remark,
                    organization.Level
                    ));
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
