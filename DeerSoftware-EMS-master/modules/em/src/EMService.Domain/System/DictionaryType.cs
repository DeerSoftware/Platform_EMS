using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public class DictionaryType : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型编码
        /// </summary>
        public string Code { get; set; }
    }
}
