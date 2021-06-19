using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace ModelingService.Host
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
            string strip = (Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + Request.HttpContext.Connection.LocalPort);

            return Task.FromResult(strip);
        }

        
    }
}
