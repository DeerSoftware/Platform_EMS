using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService
{
    /// <summary>
    /// 管辖权限
    /// </summary>
    public class EquipmentPower : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 设备Id
        /// </summary>
        public Guid EquipmentId { get; set; }
    }
}
