using EMService.System.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EMService
{
    /// <summary>
    /// 序列号服务接口实现
    /// </summary>
    public class SequenceAppService : ApplicationService, ISequenceAppService
    {
        private readonly SequenceManager _sequenceManager;
        private readonly IRepository<Sequence, Guid> _SequenceRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sequenceManager"></param>
        /// <param name="organizationManager"></param>
        /// <param name="organizationRepository"></param>
        public SequenceAppService(
            SequenceManager sequenceManager,
            IRepository<Sequence, Guid> SequenceRepository
            )
        {
            _sequenceManager = sequenceManager;
            _SequenceRepository = SequenceRepository;
        }
        /// <summary>
        /// 获取序列号
        /// 根据对象进行查询、更新、新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<SequenceDto> GetSequenceAsync<T>()
        {
            return ObjectMapper.Map<Sequence, SequenceDto>(await _sequenceManager.GetSequenceAsync<T>());
        }
    }
}
