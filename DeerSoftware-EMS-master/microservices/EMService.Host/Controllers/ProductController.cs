using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EMService.Host.Controllers
{
    [RemoteService]
    [Area("productManagement")]
    [Route("api/productManagement")]
    public class ProductsController : AbpController
    {
        [HttpGet]
        [Route("GetInfo")]
        public Task<string> GetInfoAsync()
        {
            return Task.FromResult("Success");
        }
    }
}
