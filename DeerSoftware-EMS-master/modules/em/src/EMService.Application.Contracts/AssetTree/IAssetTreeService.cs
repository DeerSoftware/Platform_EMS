using EMService.AssetTree.Dto;
using EMService.Result;
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
        /// 极据上级节点查询所有下级设备数据
        /// </summary>
        /// <param name="pNodeId">上级节点Id</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        Task<ServiceResult<List<DeviceDto>>> getChildrenDeviceData(int pNodeId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null);

        /// <summary>
        /// 极据上级节点查询所有下级测点数据
        /// </summary>
        /// <param name="pNodeId">上级节点Id</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        Task<ServiceResult<List<PointDto>>> getChildrenPointData(int pNodeId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null);

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
        /// 根据上级节点Id查询子级数据
        /// </summary>
        /// <param name="pId">上级节点Id</param>
        /// <returns></returns>
        Task<List<FoundationDto>> getAssetTreeDataByParentId(Guid pId);

        /// <summary>
        /// 新增资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task CreateAssetNode(dynamic assetData);

        /// <summary>
        /// 修改资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task UpdateAssetNode(dynamic assetData);

        /// <summary>
        /// 删除资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        Task DelAssetNode(int deviceType, Guid idKey);

        /// <summary>
        /// 获取右击菜单设备类型的数据
        /// </summary>
        /// <returns></returns>
        Task<List<PopMenuDto>> getPopMenuData();
    }

}
