using EMService.AssetTree.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService.AssetTree
{
    public class AssetTreeService : IAssetTreeService
    {

        public Task<dynamic> getAssetDataById(int deviceType, Guid idKey)
        {
            dynamic assetData = null;

            switch (deviceType)
            {
                case (int) DeviceType.Organization:
                case (int)DeviceType.Base:
                case (int)DeviceType.Factory:
                case (int)DeviceType.Area:
                case (int)DeviceType.System:

                    break;
                case (int)DeviceType.Device:
                case (int)DeviceType.Component:

                    break;
                case (int)DeviceType.Point:

                    break;


            }

            return assetData;
        }

        public Task<List<FoundationDto>> getAssetTreeData(int deviceType = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<dynamic>> getChildrenAssetDataByParentId(int deviceType, int pNodeId, string filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddOrUpdateAssetNode(dynamic assetData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DelAssetNode(Guid idKey)
        {
            throw new NotImplementedException();
        }
    }
}
