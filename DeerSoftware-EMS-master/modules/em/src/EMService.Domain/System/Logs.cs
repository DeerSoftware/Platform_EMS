using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService.System
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logs : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid Operator { get; set; }
        /// <summary>
        /// 所属功能
        /// </summary>
        public SystemType FunctionalZone { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        public ExecutiveOutcomes ExecutiveOutcomes { get; set; }

    }
}
