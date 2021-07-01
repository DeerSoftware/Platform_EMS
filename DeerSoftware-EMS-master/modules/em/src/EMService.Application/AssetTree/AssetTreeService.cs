using EMService.AssetTree.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EMService.AssetTree
{
    public class AssetTreeService : ApplicationService, IAssetTreeService
    {
        private readonly IRepository<Point, Guid> _pointRepository;
        private readonly IRepository<Device, Guid> _deviceRepository;
        private readonly IRepository<PopMenu, Guid> _popMenuRepository;
        private readonly IRepository<Foundation, Guid> _foundationRepository;

        public AssetTreeService(IRepository<Foundation, Guid> foundationRepository, IRepository<Device, Guid> deviceRepository,
            IRepository<Point, Guid> pointRepository, IRepository<PopMenu, Guid> popMenuRepository)
        {
            this._pointRepository = pointRepository;
            this._deviceRepository = deviceRepository;
            this._popMenuRepository = popMenuRepository;
            this._foundationRepository = foundationRepository;
        }

        
        public async Task<dynamic> getAssetDataById(int deviceType, Guid idKey)
        {
            dynamic assetData = null;

            var foundation = await _foundationRepository.FindAsync(b => b.Id == idKey);

            switch (deviceType)
            {
                case (int)DeviceType.Organization:
                case (int)DeviceType.Base:
                case (int)DeviceType.Factory:
                case (int)DeviceType.Area:
                case (int)DeviceType.System:

                    assetData = ObjectMapper.Map<Foundation, FoundationDto>(foundation);

                    break;
                case (int)DeviceType.Device:
                case (int)DeviceType.Component:

                    var device = await _deviceRepository.FindAsync(b => b.Id == idKey);

                    assetData = ObjectMapper.Map<(Foundation, Device), DeviceDto>((foundation, device));

                    break;
                default:

                    var point = await _pointRepository.FindAsync(b => b.Id == idKey);

                    assetData = ObjectMapper.Map<(Foundation, Point), PointDto>((foundation, point));

                    break;
            }

            return assetData;
        }

        public Task<List<FoundationDto>> getAssetTreeData(int deviceType = 10)
        {
            var foundation = _foundationRepository.Where(b => b.DeviceType <= deviceType).ToList();
            var foundationDto = ObjectMapper.Map<List<Foundation>, List<FoundationDto>>(foundation);

            return Task.FromResult(TransformTreeData(foundationDto));
        }

        public Task<List<DeviceDto>> getChildrenDeviceData(int pNodeId, string filter = null)
        {
            dynamic assetData = null;

            var foundation = _foundationRepository.Where(b => b.TreeArea.Contains(pNodeId.ToString()) &&
                                             (b.DeviceType == (int)DeviceType.Device || b.DeviceType == (int)DeviceType.Component));


            if (!string.IsNullOrWhiteSpace(filter))
            {
                foundation = foundation.Where(b => b.Name.Contains(filter) || b.Code.Contains(filter));
            }

            var deviceData = foundation.Join(_deviceRepository, b => b.Id, d => d.Id, (b, d) => new { b, d }); ;

            var deviceList = new List<Device>();
            var foundationList = new List<Foundation>();

            foreach (var item in deviceData)
            {
                deviceList.Add(item.d);
                foundationList.Add(item.b);
            }

            assetData = ObjectMapper.Map<(List<Foundation>, List<Device>), List<DeviceDto>>((foundationList, deviceList));

            return assetData;
        }

        /// <summary>
        /// 极据上级节点查询所有下级设备数据
        /// </summary>
        /// <param name="pNodeId">上级节点Id</param>
        /// <param name="filter">过滤条件</param>
        [HttpGet]
        public Task<List<PointDto>> getChildrenPointData(int pNodeId, string filter = null)
        {
            dynamic assetData = null;

            var foundation = _foundationRepository.Where(b => b.TreeArea.Contains(pNodeId.ToString()) &&
                                      (b.DeviceType >= (int)DeviceType.Observe));

            if (!string.IsNullOrWhiteSpace(filter))
            {
                foundation = foundation.Where(b => b.Name.Contains(filter) || b.Code.Contains(filter));
            }

            var pointData = foundation.Join(_pointRepository, b => b.Id, d => d.Id, (b, d) => new { b, d }).ToList();

            var pointList = new List<Point>();
            var foundationList = new List<Foundation>();

            foreach (var item in pointData)
            {
                pointList.Add(item.d);
                foundationList.Add(item.b);
            }

            assetData = ObjectMapper.Map<(List<Foundation>, List<Point>), List<PointDto>>((foundationList, pointList));

            return assetData;
        }

        public async Task CreateAssetNode(int deviceType, dynamic assetData)
        {
            var foundation = ObjectMapper.Map<FoundationDto, Foundation>(assetData);

            switch (deviceType)
            {
                case (int)DeviceType.Organization:
                case (int)DeviceType.Base:
                case (int)DeviceType.Factory:
                case (int)DeviceType.Area:
                case (int)DeviceType.System:

                    await _foundationRepository.InsertAsync(foundation);
                    break;

                case (int)DeviceType.Device:
                case (int)DeviceType.Component:

                    var device = ObjectMapper.Map<DeviceDto, Device>(assetData);

                    await _foundationRepository.InsertAsync(foundation);
                    await _deviceRepository.InsertAsync(device);

                    break;
                default:

                    var point = ObjectMapper.Map<PointDto, Point>(assetData);

                    await _foundationRepository.InsertAsync(foundation);
                    await _deviceRepository.InsertAsync(point);

                    break;
            }
        }

        public async Task UpdateAssetNode(int deviceType, FoundationDto assetData)
        {
            var foundation = await _foundationRepository.FindAsync(b => b.Id == assetData.Id);
            foundation = ObjectMapper.Map<FoundationDto, Foundation>(assetData);

            switch (deviceType)
            {
                case (int)DeviceType.Organization:
                case (int)DeviceType.Base:
                case (int)DeviceType.Factory:
                case (int)DeviceType.Area:
                case (int)DeviceType.System:

                    await _foundationRepository.UpdateAsync(foundation);

                    break;

                case (int)DeviceType.Device:
                case (int)DeviceType.Component:

                    var device = await _deviceRepository.FindAsync(b => b.Id == assetData.Id);
                    var deviceData = (DeviceDto)assetData;

                    device = ObjectMapper.Map<DeviceDto, Device>(deviceData);

                    await _foundationRepository.UpdateAsync(foundation);
                    await _deviceRepository.InsertAsync(device);

                    break;
                default:

                    var point = await _pointRepository.FindAsync(b => b.Id == assetData.Id);
                    var pointData = (PointDto)assetData;

                    point = ObjectMapper.Map<PointDto, Point>(pointData);

                    await _foundationRepository.UpdateAsync(foundation);
                    await _pointRepository.InsertAsync(point);

                    break;
            }
        }

        public async Task DelAssetNode(int deviceType, Guid idKey)
        {
            switch (deviceType)
            {
                case (int)DeviceType.Organization:
                case (int)DeviceType.Base:
                case (int)DeviceType.Factory:
                case (int)DeviceType.Area:
                case (int)DeviceType.System:

                    await _foundationRepository.DeleteAsync(idKey);

                    break;

                case (int)DeviceType.Device:
                case (int)DeviceType.Component:

                    await _foundationRepository.DeleteAsync(idKey);
                    await _deviceRepository.DeleteAsync(idKey);

                    break;
                default:

                    await _foundationRepository.DeleteAsync(idKey);
                    await _pointRepository.DeleteAsync(idKey);

                    break;
            }
        }

        public async Task<List<PopMenuDto>> getPopMenuData()
        {
            var popMenuData = await _popMenuRepository.ToListAsync();

            return ObjectMapper.Map<List<PopMenu>, List<PopMenuDto>>(popMenuData);
        }

        /// <summary>
        /// 递归第一级数据
        /// </summary>
        /// <param name="treeDataList"></param>
        /// <returns></returns>

        private List<FoundationDto> TransformTreeData(List<FoundationDto> treeDataList)
        {
            var data = treeDataList.Where(x => x.ParentId == null || !x.ParentId.HasValue);

            List<FoundationDto> list = new List<FoundationDto>();
            foreach (var item in data)
            {
                TransformChildTreeData(treeDataList, item);
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 递归子级数据
        /// </summary>
        /// <param name="treeDataList">树形列表数据</param>
        /// <param name="parentItem">父级model</param>
        private void TransformChildTreeData(List<FoundationDto> treeDataList, FoundationDto parentItem)
        {
            var subItems = treeDataList.Where(ee => ee.ParentId == parentItem.Id).ToList();
            if (subItems.Count != 0)
            {
                parentItem.TreeChildren = new List<FoundationDto>();
                parentItem.TreeChildren.AddRange(subItems);
                foreach (var subItem in subItems)
                {
                    TransformChildTreeData(treeDataList, subItem);
                }
            }
        }
    }
}
