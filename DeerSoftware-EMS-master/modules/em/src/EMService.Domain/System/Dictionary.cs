using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService
{
    /// <summary>
    /// 字典实体
    /// </summary>
    public class Dictionary : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public Guid DictionaryType { get; set; }

    }
}
