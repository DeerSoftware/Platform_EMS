
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Newtonsoft.Json;

using EMService.AssetTree.Dto;
using EMService.Extensions;

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

        public async Task<List<FoundationDto>> getAssetTreeData(int deviceType = 10)
        {
            var foundation = await _foundationRepository.Where(b => b.DeviceType <= deviceType).OrderBy(b => b.Sort).ToListAsync();
            var foundationDto = ObjectMapper.Map<List<Foundation>, List<FoundationDto>>(foundation);

            return TransformTreeData(foundationDto);
        }

        public async Task<List<FoundationDto>> getAssetTreeDataByParentId(Guid pId)
        {
            var foundation = await _foundationRepository.Where(b => b.ParentId == pId).OrderBy(b => b.Sort).ToListAsync();
            var foundationDto = ObjectMapper.Map<List<Foundation>, List<FoundationDto>>(foundation);

            return foundationDto;
        }

        public async Task<List<DeviceDto>> getChildrenDeviceData(int pNodeId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null)
        {
            List<DeviceDto> assetData = new List<DeviceDto>();

            var foundation = await _foundationRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(b => b.TreeArea.Contains(pNodeId.ToString()) &&
                                            (b.DeviceType == (int)DeviceType.Device || b.DeviceType == (int)DeviceType.Component)).Include(p => p.Device);

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    query = query.Where(b => b.Name.Contains(filter) || b.Code.Contains(filter));
                }

                return query;

            }, pageIndex, pageSize);

            //var deviceData = foundation.Join(_deviceRepository, b => b.Id, d => d.Id, (b, d) => new { b, d }).ToList();

            foreach (var item in foundation)
            {
                var deviceDto = ObjectMapper.Map<(Foundation, Device), DeviceDto>((item, item.Device));
                assetData.Add(deviceDto);
            }

            return assetData;
        }

        /// <summary>
        /// 极据上级节点查询所有下级设备数据
        /// </summary>
        /// <param name="pNodeId">上级节点Id</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public async Task<List<PointDto>> getChildrenPointData(int pNodeId, int pageIndex = 1, int pageSize = int.MaxValue, string filter = null)
        {
            List<PointDto> assetData = new List<PointDto>();

            var foundation = await _foundationRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(b => b.TreeArea.Contains(pNodeId.ToString()) &&
                                (b.DeviceType >= (int)DeviceType.Observe)).Include(p => p.Point);

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    query = query.Where(b => b.Name.Contains(filter) || b.Code.Contains(filter));
                }

                return query;

            }, pageIndex, pageSize);

            //var pointData = foundation.Join(_pointRepository, b => b.Id, d => d.Id, (b, d) => new { b, d }).ToList();

            foreach (var item in foundation)
            {
                var pointDto = ObjectMapper.Map<(Foundation, Point), PointDto>((item, item.Point));
                assetData.Add(pointDto);
            }

            return assetData;
        }

        public async Task CreateAssetNode(dynamic assetData)
        {
            if (assetData is JsonElement)
            {
                var foundationDto = JsonConvert.DeserializeObject<FoundationDto>(assetData.ToString());

                string key = Guid.NewGuid().ToString("D");
                foundationDto.Id = Guid.Parse(key);

                var foundation = ObjectMapper.Map<FoundationDto, Foundation>(foundationDto);

                switch (foundation.DeviceType)
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

                        var deviceDto = JsonConvert.DeserializeObject<DeviceDto>(assetData.ToString());
                        deviceDto.Id = Guid.Parse(key);
                        var device = ObjectMapper.Map<DeviceDto, Device>(deviceDto);

                        await _foundationRepository.InsertAsync(foundation);
                        await _deviceRepository.InsertAsync(device);

                        break;
                    default:

                        var pointDto = JsonConvert.DeserializeObject<DeviceDto>(assetData.ToString());
                        pointDto.Id = Guid.Parse(key);
                        var point = ObjectMapper.Map<PointDto, Point>(pointDto);

                        await _foundationRepository.InsertAsync(foundation);
                        await _pointRepository.InsertAsync(point);

                        break;
                }
            }

        }

        public async Task UpdateAssetNode(dynamic assetData)
        {
            if (assetData is JsonElement)
            {
                FoundationDto foundationDto = JsonConvert.DeserializeObject<FoundationDto>(assetData.ToString());
                var foundation = await _foundationRepository.FindAsync(b => b.Id == foundationDto.Id);

                foundation.ParentId = foundationDto.ParentId;
                foundation.Name = foundationDto.Name;
                foundation.Code = foundationDto.Code;
                foundation.JianPin = foundationDto.JianPin;
                foundation.Sort = foundationDto.Sort;

                switch (foundation.DeviceType)
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

                        DeviceDto deviceDto = JsonConvert.DeserializeObject<DeviceDto>(assetData.ToString());
                        var device = await _deviceRepository.FindAsync(b => b.Id == deviceDto.Id);

                        device.LocationCode = deviceDto.LocationCode;
                        device.ErpCode = deviceDto.ErpCode;
                        device.Specialty = deviceDto.Specialty;
                        device.ControlLevel = deviceDto.ControlLevel;
                        device.DeviceCategory = deviceDto.DeviceCategory;
                        device.DeviceKind = deviceDto.DeviceKind;
                        device.Profession = deviceDto.Profession;
                        device.Spec = deviceDto.Spec;
                        device.Model = deviceDto.Model;
                        device.MeasureUnit = deviceDto.MeasureUnit;
                        device.ResponsibleUserId = deviceDto.ResponsibleUserId;
                        device.ResponsibleEngineer = deviceDto.ResponsibleEngineer;
                        device.UsedState = deviceDto.UsedState;
                        device.Description = deviceDto.Description;

                        await _foundationRepository.UpdateAsync(foundation);
                        await _deviceRepository.UpdateAsync(device);

                        break;
                    default:

                        PointDto pointDto = JsonConvert.DeserializeObject<PointDto>(assetData.ToString());
                        var point = await _pointRepository.FindAsync(b => b.Id == pointDto.Id);

                        point.FixedCode = pointDto.FixedCode;
                        point.ProcessCode = pointDto.ProcessCode;
                        point.Specialty = pointDto.Specialty;
                        point.ControlLevel = pointDto.ControlLevel;
                        point.AccessMode = pointDto.AccessMode;
                        point.UnitType = pointDto.UnitType;
                        point.EngineeringUnit = pointDto.EngineeringUnit;
                        point.MeasWay = pointDto.MeasWay;
                        point.MeasureDirect = pointDto.MeasureDirect;
                        point.IsStoppingSignal = pointDto.IsStoppingSignal;
                        point.MaxValue = pointDto.MaxValue;
                        point.MinValue = pointDto.MinValue;
                        point.ReferenceValue = pointDto.ReferenceValue;
                        point.EnergyType = pointDto.EnergyType;

                        await _foundationRepository.UpdateAsync(foundation);
                        await _pointRepository.UpdateAsync(point);

                        break;
                }
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
            var popMenuData = await _popMenuRepository.OrderBy(b => b.Sort).ToListAsync();

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