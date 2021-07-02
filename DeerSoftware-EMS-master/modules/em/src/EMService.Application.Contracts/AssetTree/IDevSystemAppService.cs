using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EMService
{
    public interface IDevSystemAppService : IApplicationService
    {
        /// <summary>
        /// 根据主键Id查询资产数据
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        Task<dynamic> getAssetDataById(int deviceType, Guid idKey);
        /// <summary>
        /// 根据父级查询子级
        /// </summary>
        /// <returns></returns>
        Task<List<dynamic>> getAssetDataByParentId(Guid idKey);

        /// <summary>
        /// 新增或修改资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task AddOrUpdateAssetNode(dynamic assetData);

        /// <summary>
        /// 删除资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task DelAssetNode(Guid idKey);
    }
}
