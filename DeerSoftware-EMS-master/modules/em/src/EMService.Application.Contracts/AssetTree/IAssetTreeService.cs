using EMService.AssetTree.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EMService.AssetTree
{
    /// <summary>
    /// 资产设备树的接口服务
    /// </summary>
    public interface IAssetTreeService : IApplicationService
    {
        /// <summary>
        /// 极据上级节点查询资产数据,包括设备和测点数据
        /// </summary>
        /// <param name="DeviceType">设备类型</param>
        /// <param name="pNodeId">上级节点Id</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        Task<List<dynamic>> getChildrenAssetDataByParentId(int deviceType, int pNodeId, string filter = null);

        /// <summary>
        /// 根据主键Id查询资产数据
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        Task<dynamic> getAssetDataById(int deviceType, Guid idKey);

        /// <summary>
        /// 获取资产树数据
        /// </summary>
        /// <returns></returns>
        Task<List<FoundationDto>> getAssetTreeData(int deviceType = 10);

        /// <summary>
        /// 新增或修改资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task<bool> AddOrUpdateAssetNode(dynamic assetData);

        /// <summary>
        /// 删除资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task<bool> DelAssetNode(Guid idKey);
    }

}
