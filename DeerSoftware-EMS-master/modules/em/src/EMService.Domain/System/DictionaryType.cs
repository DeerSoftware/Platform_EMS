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
    /// 字典类型
    /// </summary>
    [Table("EMS_Sys_DictionaryType")]
    public class DictionaryType : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [NotNull]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 类型编码
        /// </summary>
        [NotNull]
        [Required]
        public string Code { get; set; }
    }
}
