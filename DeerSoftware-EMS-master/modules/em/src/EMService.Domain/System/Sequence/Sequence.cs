using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService
{
    /// <summary>
    /// 序列数据对象
    /// </summary>
    public class Sequence : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        public string TableName { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public int Value { get; set; }
    }
}
