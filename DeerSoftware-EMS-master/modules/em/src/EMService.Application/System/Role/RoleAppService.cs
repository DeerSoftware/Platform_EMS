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
    public class RoleAppService : ApplicationService, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role, Guid> _roleRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="roleRepository"></param>
        public RoleAppService(
            RoleManager roleManager,
            IRepository<Role, Guid> roleRepository
            )
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
        }
        /// <summary>
        /// 创建组织对象
        /// </summary>
        /// <param name="input">组织对象</param>
        /// <returns></returns>
        public async Task<Result<RoleDto>> CreateAsync(CreateRoleDto input)
        {
            Result<RoleDto> result = new Result<RoleDto>();
            Role role = default;
            try
            {
                var roleName = await _roleRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Name));
                if (roleName != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.Name);
                }
                var roleCode = await _roleRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Code));
                if (roleCode != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.Code);
                }
                role = ObjectMapper.Map<CreateRoleDto, Role>(input);
                var data = ObjectMapper.Map<Role, RoleDto>(await _roleManager.CreateAsync(role));

                result.Code = "930001";
                result.Message = "创建成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "930001";
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
                await _roleRepository.DeleteAsync(id);

                result.Code = "930002";
                result.Message = "删除成功";
                result.ResultType = ResultType.Succeed;
            }
            catch (Exception)
            {
                result.Code = "930002";
                result.Message = "删除成功,失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 根据Id查询组织对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <returns></returns>
        public async Task<Result<RoleDto>> GetAsync(Guid id)
        {
            Result<RoleDto> result = new Result<RoleDto>();
            try
            {
                Role role = await _roleRepository.FirstOrDefaultAsync(p => p.Id == id);
                var data = ObjectMapper.Map<Role, RoleDto>(role);

                result.Code = "930003";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "930003";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }

            return result;
        }
        /// <summary>
        /// 查询角色对象列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<ListResultDto<RoleDto>>> GetListAsync()
        {
            Result<ListResultDto<RoleDto>> result = new Result<ListResultDto<RoleDto>>();
            try
            {
                var roles = await _roleRepository.GetListAsync();
                var roleDtos = ObjectMapper.Map<List<Role>, List<RoleDto>>(roles);
                var data = new ListResultDto<RoleDto>(roleDtos);

                result.Code = "930004";
                result.Message = "查询失败,编码为：" + result.Code;
                result.ResultType = ResultType.Succeed;
            }
            catch (Exception)
            {
                result.Code = "930004";
                result.Message = "查询成功";
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        /// <summary>
        /// 带分页查询组织对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Result<PagedResultDto<RoleDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<RoleDto>> result = new Result<PagedResultDto<RoleDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                var roles = await _roleRepository
                    .OrderBy(input.Sorting ?? "Name")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();
                var totalCount = await _roleRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<Role>, List<RoleDto>>(roles);
                var data = new PagedResultDto<RoleDto>(totalCount, dtos);

                result.Code = "930005";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "930005";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }
        /// <summary>
        /// 更新角色对象
        /// </summary>
        /// <param name="id">组织Id</param>
        /// <param name="input">更新实体</param>
        /// <returns></returns>
        public async Task<Result<RoleDto>> UpdateAsync(UpdateRoleDto input)
        {
            Result<RoleDto> result = new Result<RoleDto>();
            try
            {
                var role = await _roleRepository.GetAsync(input.Id);
                role.Name = input.Name;
                role.Code = input.Code;
                role.Status = input.Status;
                role.Remark = input.Remark;

                var data = ObjectMapper.Map<Role, RoleDto>(role);

                result.Code = "930006";
                result.Message = "更新成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "930006";
                result.Message = "更新失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
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
