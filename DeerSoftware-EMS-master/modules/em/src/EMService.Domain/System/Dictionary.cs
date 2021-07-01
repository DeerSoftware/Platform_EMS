using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace EMService
{
    /// <summary>
    /// 字典实体
    /// </summary>
    [Table("EMS_Sys_Dictionary")]
    public class Dictionary : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [NotNull]
        [Required] 
        public string Name { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        [NotNull]
        [Required] 
        public string Code { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        [NotNull]
        [Required] 
        public Guid DictionaryType { get; set; }

    }
}
