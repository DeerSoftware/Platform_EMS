using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMService.System.Price
{
    public class PriceAppService : ApplicationService, IPriceAppService
    {
        private readonly SequenceManager _sequenceManager;
        private readonly PriceManager _priceManager;
        private readonly IRepository<Price, Guid> _priceRepository;

        public PriceAppService(
            SequenceManager sequenceManager,
            PriceManager priceManager,
            IRepository<Price, Guid> priceRepository)
        {
            _sequenceManager = sequenceManager;
            _priceManager = priceManager;
            _priceRepository = priceRepository;
        }

        public async Task<Result<PriceDto>> CreateAsync(CreatePriceDto input)
        {
            Result<PriceDto> result = new Result<PriceDto>();
            Price price = default;
            try
            {
                var typeCode = await _priceRepository.FirstOrDefaultAsync(p => p.TypeCode.Equals(input.TypeCode));
                if (typeCode != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(input.TypeCode);
                }

                price = ObjectMapper.Map<CreatePriceDto, Price>(input);
                var data = ObjectMapper.Map<Price, PriceDto>(await _priceManager.CreateAsync(price));

                result.Code = "990001";
                result.Message = "创建成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "990001";
                result.Message = "创建失败,失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;

            }
            return result;
        }

        public async Task<Result<int>> DeleteAsync(Guid id)
        {
            Result<int> result = new Result<int>();
            try
            {
                await _priceRepository.DeleteAsync(id);
                result.Code = "990002";
                result.Message = "删除成功";
                result.ResultType = ResultType.Succeed;
            }
            catch (Exception)
            {
                result.Code = "990002";
                result.Message = "删除成功,失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        public async Task<Result<PriceDto>> GetAsync(Guid id)
        {
            Result<PriceDto> result = new Result<PriceDto>();
            try
            {
                Price price = await _priceRepository.FirstOrDefaultAsync(p => p.Id == id);
                if (price == null)
                {
                    result.Code = "990003";
                    result.Message = "查询数据不存在";
                    result.ResultType = ResultType.Error;
                    return result;
                }
                var data = ObjectMapper.Map<Price, PriceDto>(price);

                result.Code = "990003";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "990003";
                result.Message = "查询失败,失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        public async Task<Result<PagedResultDto<PriceDto>>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<PriceDto>> result = new Result<PagedResultDto<PriceDto>>();
            try
            {
                await NormalizeMaxResultCountAsync(input);

                var prices = await _priceRepository
                    .OrderBy(input.Sorting ?? "BaseName")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _priceRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<Price>, List<PriceDto>>(prices);
                var data = new PagedResultDto<PriceDto>(totalCount, dtos);

                result.Code = "990005";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "990005";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        public async Task<Result<PagedResultDto<PriceDto>>> GetListPagedByIdAsync(Guid BaseId, PagedAndSortedResultRequestDto input)
        {
            Result<PagedResultDto<PriceDto>> result = new Result<PagedResultDto<PriceDto>>();
            try
            {

                //List<Price> prices = default;
                //Expression<Func<Price, bool>> where = p => p.BaseId == BaseId;
                //prices = await _priceRepository.GetListAsync(where);
                //var menuList = ObjectMapper.Map<List<Price>, List<PriceDto>>(prices);
                //var data = new ListResultDto<PriceDto>(menuList);

                await NormalizeMaxResultCountAsync(input);
                var prices = await _priceRepository.Where(p => p.BaseId == BaseId)
                    .OrderBy(input.Sorting ?? "BaseName")
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                var totalCount = await _priceRepository.GetCountAsync();
                var dtos = ObjectMapper.Map<List<Price>, List<PriceDto>>(prices);
                var data = new PagedResultDto<PriceDto>(totalCount, dtos);

                result.Code = "990006";
                result.Message = "查询成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "990006";
                result.Message = "查询失败，失败编码为：" + result.Code;
                result.ResultType = ResultType.Error;
            }
            return result;
        }

        public async Task<Result<PriceDto>> UpdateAsync(UpdatePriceDto input)
        {
            Result<PriceDto> result = new Result<PriceDto>();
            try
            {
                var price = await _priceRepository.GetAsync(input.Id);
                price.BaseId = input.BaseId;
                price.BaseName = input.BaseName;
                price.PriceType = input.PriceType;
                price.PriceTypeName = input.PriceTypeName;
                price.UnitPrice = input.UnitPrice;
                price.TypeCode = input.TypeCode;
                var data = ObjectMapper.Map<Price, PriceDto>(price);
                result.Code = "990007";
                result.Message = "更新成功";
                result.ResultType = ResultType.Succeed;
                result.Data = data;
            }
            catch (Exception)
            {
                result.Code = "990007";
                result.Message = "更新失败，失败代码：" + result.Code;
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
