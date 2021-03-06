using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMService
{
    /// <summary>
    /// 角色权限关系
    /// </summary>
    [Table("EMS_Sys_RolePower")]
    public class RolePower : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }

    }
}
