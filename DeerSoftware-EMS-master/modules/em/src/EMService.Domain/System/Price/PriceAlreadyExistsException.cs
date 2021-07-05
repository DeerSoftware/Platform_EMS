using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EMService.System.Price
{
   public class PriceAlreadyExistsException: BusinessException
    {
        public PriceAlreadyExistsException(string priceCode) : base("PM:000001", $"A Organization with code {priceCode} has already exists!")
        {

        }
    }
}
