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

namespace EMService
{
    /// <summary>
    /// 组织服务接口实现
    /// </summary>
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        private readonly SequenceManager _sequenceManager;
        private readonly OrganizationManager _organizationManager;
        private readonly IRepository<Organization, int> _OrganizationRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sequenceManager"></param>
        /// <param name="organizationManager"></param>
        /// <param name="organizationRepository"></param>
        public OrganizationAppService(
            SequenceManager sequenceManager,
            OrganizationManager organizationManager,
            IRepository<Organization, int> organizationRepository
            )
        {
            _sequenceManager = sequenceManager;
            _OrganizationRepository = organizationRepository;
            _organizationManager = organizationManager;
        }
        /// <summary>
        /// 创建组织对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto input)
        {
            var existingOrganization = await _OrganizationRepository.GetAsync(p => p.OrgCode == input.OrgCode);
            if (existingOrganization != null)
            {
                throw new OrganizationCodeAlreadyExistsException(input.OrgCode);
            }
            var sequence = await _sequenceManager.GetSequenceAsync<Organization>();

            Organization organization = ObjectMapper.Map<CreateOrganizationDto, Organization>(input);

            return ObjectMapper.Map<Organization, OrganizationDto>(await _organizationManager.CreateAsync(organization, sequence.Value));
        }
        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await _OrganizationRepository.DeleteAsync(id);
        }
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<OrganizationDto> GetAsync(int id)
        {
            var organization = await _OrganizationRepository.GetAsync(id);
            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }
        /// <summary>
        /// 查询组织对象列表
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<OrganizationDto>> GetListAsync()
        {
            var organizations = await _OrganizationRepository.GetListAsync();
            var organizationList = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(organizations);
            return new ListResultDto<OrganizationDto>(organizationList);
        }

        /// <summary>
        /// 带分页查询组织对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrganizationDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            await NormalizeMaxResultCountAsync(input);

            var organizations = await _OrganizationRepository
                .OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var totalCount = await _OrganizationRepository.GetCountAsync();

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(organizations);

            return new PagedResultDto<OrganizationDto>(totalCount, dtos);
        }
        /// <summary>
        /// 更新组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        public async Task<OrganizationDto> UpdateAsync(int id, UpdateOrganizationDto input)
        {
            var organization = await _OrganizationRepository.GetAsync(id);

            organization.OrgName = input.OrgName;
            organization.OrgNickName = input.OrgNickName;
            organization.ParentId = input.ParentId;
            organization.OrgCode = input.OrgCode;
            organization.Phone = input.Phone;
            organization.PhoneExt = input.PhoneExt;
            organization.Email = input.Email;
            organization.Sort = input.Sort;
            organization.ResponsiblePerson = input.ResponsiblePerson;
            organization.Address = input.Address;
            organization.Remark = input.Remark;

            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }
        #region
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
