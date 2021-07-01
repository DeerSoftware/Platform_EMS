using EMService.AssetTree;
using EMService.AssetTree.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace EMService
{
    public class DevSystemAppService : ApplicationService, IDevSystemAppService
    {

        private readonly IRepository<DevSystem, Guid> _DevSystemRepository;
        private readonly IRepository<Foundation, Guid> _FoundationsRepository;

        public DevSystemAppService(IRepository<DevSystem, Guid> devSystemRepository, IRepository<Foundation, Guid> foundationRepository)
        {
            _DevSystemRepository = devSystemRepository;
            _FoundationsRepository = foundationRepository;
        }

        public Task AddOrUpdateAssetNode(dynamic assetData)
        {
            throw new NotImplementedException();
        }

        public async Task DelAssetNode(Guid idKey)
        {
            var device =await _DevSystemRepository.GetAsync(p => p.Id == idKey);

            if (device==null)
            {
                throw new UserFriendlyException("当前数据不存在！");
            }

            var chrildDevice = getAssetDataByParentId(idKey);

            if (chrildDevice.Result.Count>0)
            {
                throw new UserFriendlyException("当前数据存在子级！");
            }
            await _DevSystemRepository.DeleteAsync(idKey);
        }

        public Task<dynamic> getAssetDataById(int deviceType, Guid idKey)
        {
            throw new NotImplementedException();
        }

        public async Task<List<dynamic>> getAssetDataByParentId(Guid idKey)
        {
            List<dynamic> os = new List<dynamic>();
            var list = await _FoundationsRepository.GetListAsync();
            var query = list.Where(p => p.ParentId == idKey).ToList();
            foreach(var item in query)
            {
                os.Add(item);
            }
            return  os;
        }
    }
}
