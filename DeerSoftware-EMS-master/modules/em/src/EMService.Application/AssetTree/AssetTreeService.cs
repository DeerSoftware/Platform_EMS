
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
using EMService.Core.Extensions;
using EMService.Result;
using Microsoft.AspNetCore.Mvc;

namespace EMService.AssetTree
{
    /// <summary>
    /// 资产设备树服务
    /// </summary>
    public class AssetTreeService : EMServiceAppService, IAssetTreeService
    {
        private readonly IRepository<Point, Guid> _pointRepository;
        private readonly IRepository<Device, Guid> _deviceRepository;
        private readonly IRepository<PopMenu, Guid> _popMenuRepository;
        private readonly IRepository<Foundation, Guid> _foundationRepository;

        /// <summary>
        /// 资产设备树构造器
        /// </summary>
        /// <param name="foundationRepository"></param>
        /// <param name="deviceRepository"></param>
        /// <param name="pointRepository"></param>
        /// <param name="popMenuRepository"></param>
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

                    assetData = this.Map<Foundation, FoundationDto>(foundation);

                    break;
                case (int)DeviceType.Device:
                case (int)DeviceType.Component:

                    var device = await _deviceRepository.FindAsync(b => b.Id == idKey);

                    assetData = this.Map<(Foundation, Device), DeviceDto>((foundation, device));

                    break;
                default:

                    var point = await _pointRepository.FindAsync(b => b.Id == idKey);

                    assetData = this.Map<(Foundation, Point), PointDto>((foundation, point));

                    break;
            }

            return assetData;
        }

        public async Task<List<FoundationDto>> getAssetTreeData(int deviceType = 10)
        {
            var foundation = await _foundationRepository.Where(b => b.DeviceType <= deviceType).OrderBy(b => b.Sort).ToListAsync();
            var foundationDto = this.Map<List<Foundation>, List<FoundationDto>>(foundation);

            return TransformTreeData(foundationDto);
        }

        public async Task<List<FoundationDto>> getAssetTreeDataByParentId(Guid pId)
        {
            var foundation = await _foundationRepository.Where(b => b.ParentId == pId).OrderBy(b => b.Sort).ToListAsync();
            var foundationDto = this.Map<List<Foundation>, List<FoundationDto>>(foundation);

            return foundationDto;
        }

        /// <summary>
        /// 极据上级节点查询所有下级设备数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<List<DeviceDto>>> getChildrenDeviceData(ParameterInfo parameter)
        {
            //必填项
            var pNodeId = parameter.Filters.GetOrError<int>("pNodeId");

            var filter = parameter.Filters.GetOrDefault<string>("filter");
            var pageIndex = parameter.PageInfos.GetOrError<int>("pageIndex");
            var pageSize = parameter.PageInfos.GetOrError<int>("pageSize");

            List<DeviceDto> assetData = new List<DeviceDto>();

            var foundation = await _foundationRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(b => b.TreeArea.Contains(pNodeId.ToString()) &&
                                            (b.DeviceType == (int)DeviceType.Device || b.DeviceType == (int)DeviceType.Component)).Include(p => p.Device).OrderByOrThenBy(parameter.OrderBySorts);

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    query = query.Where(b => b.Name.Contains(filter) || b.Code.Contains(filter));
                }

                return query;

            }, pageIndex, pageSize);

            //var deviceData = foundation.Join(_deviceRepository, b => b.Id, d => d.Id, (b, d) => new { b, d }).ToList();

            foreach (var item in foundation)
            {
                var deviceDto = this.Map<(Foundation, Device), DeviceDto>((item, item.Device));
                assetData.Add(deviceDto);
            }

            return ServiceResultCode.Succeed.ServiceResultSuccess(assetData);
        }

        /// <summary>
        /// 极据上级节点查询所有下级测点数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<List<PointDto>>> getChildrenPointData(ParameterInfo parameter)
        {
            //必填项
            var pNodeId = parameter.Filters.GetOrError<int>("pNodeId");

            var filter = parameter.Filters.GetOrDefault<string>("filter");
            var pageIndex = parameter.PageInfos.GetOrError<int>("pageIndex");
            var pageSize = parameter.PageInfos.GetOrError<int>("pageSize");

            List<PointDto> assetData = new List<PointDto>();

            var foundation = await _foundationRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(b => b.TreeArea.Contains(pNodeId.ToString()) &&
                                (b.DeviceType >= (int)DeviceType.Observe)).Include(p => p.Point);



                if (filter.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = query.Where(b => b.Name.Contains(filter) || b.Code.Contains(filter));
                }

                return query;

            }, pageIndex, pageSize);

            //var pointData = foundation.Join(_pointRepository, b => b.Id, d => d.Id, (b, d) => new { b, d }).ToList();

            foreach (var item in foundation)
            {
                var pointDto = this.Map<(Foundation, Point), PointDto>((item, item.Point));
                assetData.Add(pointDto);
            }

            return ServiceResultCode.Succeed.ServiceResultSuccess(assetData);
        }

        /// <summary>
        /// 新增资产节点数据
        /// </summary>
        /// <param name="assetData"></param>
        /// <returns></returns>
        public async Task CreateAssetNode(ParameterInfo<string> assetData)
        {
            if (assetData.Model.IsNotNullOrEmptyOrWhiteSpace())
            {
                var foundationDto = JsonConvert.DeserializeObject<FoundationDto>(assetData.Model);

                string key = Guid.NewGuid().ToString("D");
                foundationDto.Id = Guid.Parse(key);

                var foundation = this.Map<FoundationDto, Foundation>(foundationDto);

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

                        var deviceDto = JsonConvert.DeserializeObject<DeviceDto>(assetData.Model);
                        deviceDto.Id = Guid.Parse(key);
                        var device = this.Map<DeviceDto, Device>(deviceDto);

                        await _foundationRepository.InsertAsync(foundation);
                        await _deviceRepository.InsertAsync(device);

                        break;
                    default:

                        var pointDto = JsonConvert.DeserializeObject<PointDto>(assetData.Model);
                        pointDto.Id = Guid.Parse(key);
                        var point = this.Map<PointDto, Point>(pointDto);

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

            return this.Map<List<PopMenu>, List<PopMenuDto>>(popMenuData);
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