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
        public async Task<Result<OrganizationDto>> CreateAsync(CreateOrganizationDto input)
        {
            Result<OrganizationDto> result = new Result<OrganizationDto>();

            Organization organization = default;
            Sequence sequence = default;
            try
            {
                var existingOrganization = await _OrganizationRepository.GetListAsync(p => p.OrgCode.Equals(input.OrgCode));
                if (existingOrganization.FirstOrDefault() != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.OrgCode);
                }
                sequence = await _sequenceManager.GetSequenceAsync<Organization>();
                organization = ObjectMapper.Map<CreateOrganizationDto, Organization>(input);

                var parentOrg = await _OrganizationRepository.FirstOrDefaultAsync(p => p.Id == organization.ParentId);

                //其它数据
                organization.Level = parentOrg == null ? sequence.Value + "," : parentOrg.Level + sequence.Value + ",";

                var data = ObjectMapper.Map<Organization, OrganizationDto>(await _organizationManager.CreateAsync(organization, sequence.Value));

                result.Code = "910001";
                result.Message = "新增成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "910001";
                result.Message = "新增失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteAsync(int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                await _OrganizationRepository.DeleteAsync(id);

                result.Code = "910002";
                result.Message = "删除成功";
                result.ResultType = ResultType.Succeed;
            }
            catch (Exception)
            {
                result.Code = "910002";
                result.Message = "删除失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<OrganizationDto>> GetAsync(int id)
        {
            Result<OrganizationDto> result = new Result<OrganizationDto>();
            try
            {
                Organization organization = await _OrganizationRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<Organization, OrganizationDto>(organization);

                result.Code = "910003";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "910003";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 查询组织对象列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<ListResultDto<OrganizationDto>>> GetListAsync(string keyword)
        {
            Result<ListResultDto<OrganizationDto>> result = new Result<ListResultDto<OrganizationDto>>();

            try
            {
                var organizations = await _OrganizationRepository.GetListAsync();
                var organizationList = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(organizations);
                var data = new ListResultDto<OrganizationDto>(organizationList);

                result.Code = "910004";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "910004";
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
        public async Task<Result<PagedResultDto<OrganizationDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input, int parentId)
        {
            Result<PagedResultDto<OrganizationDto>> result = new Result<PagedResultDto<OrganizationDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                Expression<Func<Organization, bool>> where = p => p.IsDeleted == false;
                if (parentId <= 0)
                {
                    result.Code = "910005";
                    result.Message = "上级组织Id不可为空";
                    result.ResultType = ResultType.Succeed;
                    return result;
                }
                where = where.And(p => p.ParentId == parentId);

                var organizations = await _OrganizationRepository
                    .Where(where)
                    .OrderBy(input.Sorting ?? "Name")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _OrganizationRepository.CountAsync(where);

                var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(organizations);

                var data = new PagedResultDto<OrganizationDto>(totalCount, dtos);

                result.Code = "910005";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "910005";
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
        public async Task<Result<OrganizationDto>> UpdateAsync(int id, UpdateOrganizationDto input)
        {
            Result<OrganizationDto> result = new Result<OrganizationDto>();
            try
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
                var data = ObjectMapper.Map<Organization, OrganizationDto>(organization);

                result.Code = "910006";
                result.Message = "更新成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "910006";
                result.Message = "更新失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        /// <summary>
        /// 查询组织树数据
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<OrganizationTreeDto>>> GetOrganizationTree()
        {
            Result<List<OrganizationTreeDto>> result = new Result<List<OrganizationTreeDto>>();

            try
            {
                List<Organization> organizations = await _OrganizationRepository.GetListAsync(p => p.IsDeleted == false);

                var data=await GetOrganizationTreeNode(organizations);

                result.Code = "910006";
                result.Message = "更新成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;

            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        #region

        /// <summary>
        /// 组装树型结构
        /// </summary>
        /// <returns></returns>
        private async Task<List<OrganizationTreeDto>> GetOrganizationTreeNode(List<Organization> organizations)
        {

            List<OrganizationTreeDto> fNodes = organizations.Where(p => p.ParentId == 0).Select(p => new OrganizationTreeDto()
            {
                lable = string.IsNullOrEmpty(p.OrgNickName) ? p.OrgName : p.OrgNickName,
                value = p.Id,
                parentId = p.ParentId
            }).ToList();

            foreach (OrganizationTreeDto item in fNodes)
            {
                GetTreeNodeItems(item, organizations);
            }
            return fNodes;
        }
        /// <summary>
        /// 处理节点里子节点
        /// </summary>
        /// <param name="menuTreeDto"></param>
        /// <param name="menus"></param>
        private OrganizationTreeDto GetTreeNodeItems(OrganizationTreeDto organization, List<Organization> menus)
        {
            List<OrganizationTreeDto> parents = menus.Where(p => p.ParentId == organization.value).Select(p => new OrganizationTreeDto()
            {
                lable = string.IsNullOrEmpty(p.OrgNickName) ? p.OrgName : p.OrgNickName,
                value = p.Id,
                parentId = p.ParentId
            }).ToList();

            if (parents.Count > 0)
            {
                foreach (OrganizationTreeDto item in parents)
                {
                    organization.children.Add(GetTreeNodeItems(item, menus));
                }
            }
            return organization;
        }



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
