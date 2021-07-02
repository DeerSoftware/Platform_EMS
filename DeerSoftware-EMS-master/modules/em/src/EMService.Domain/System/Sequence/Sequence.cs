using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EMService
{
    /// <summary>
    /// 序列数据对象
    /// </summary>
    [Table("EMS_Sys_Sequence")]
    public class Sequence : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 表名
        /// </summary>
        [NotNull]
        [Required]
        public string TableName { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [Required] 
        public int Value { get; set; }
    }
}
