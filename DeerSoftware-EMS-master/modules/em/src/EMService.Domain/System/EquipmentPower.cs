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
    /// 管辖权限
    /// </summary>
    [Table("EMS_Sys_EquipmentPower")]
    public class EquipmentPower : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        [NotNull]
        [Required]
        public Guid AccountId { get; set; }
        /// <summary>
        /// 设备Id
        /// </summary>
        [NotNull]
        [Required] 
        public Guid EquipmentId { get; set; }
    }
}
