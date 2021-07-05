using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EMService.System.Price
{
   public class PriceManager : DomainService
    {
        private readonly IRepository<Price, Guid> _priceRepository;

        public PriceManager(IRepository<Price, Guid> PriceRepository)
        {
             _priceRepository= PriceRepository;
        }

        public async Task<Price> CreateAsync(Price price)
        {
            try
            {
                var existingProduct = await _priceRepository.FirstOrDefaultAsync(p => p.TypeCode.Equals(price.TypeCode));
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(price.PriceType);
                }
                return await _priceRepository.InsertAsync(new Price(
                        GuidGenerator.Create(),
                        price.PriceType,
                        price.PriceTypeName,
                        price.TypeCode,
                        price.UnitPrice,
                        price.BaseId,
                        price.BaseName
                    )); ;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
