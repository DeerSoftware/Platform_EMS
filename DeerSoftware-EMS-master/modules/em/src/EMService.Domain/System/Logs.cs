using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EMService.System
{
    /// <summary>
    /// 日志
    /// </summary>
    [Table("EMS_Sys_Logs")]
    public class Logs : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        [NotNull]
        [Required]
        public LogType LogType { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        [NotNull]
        [Required]
        public string IpAddress { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [NotNull]
        [Required]
        public Guid Operator { get; set; }
        /// <summary>
        /// 所属功能
        /// </summary>
        [NotNull]
        [Required]
        public SystemType FunctionalZone { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        [NotNull]
        [Required]
        public ExecutiveOutcomes ExecutiveOutcomes { get; set; }
        /// <summary>
        /// 执行明细
        /// </summary>
        public string ExecutiveDetail { get; set; }

    }
}
