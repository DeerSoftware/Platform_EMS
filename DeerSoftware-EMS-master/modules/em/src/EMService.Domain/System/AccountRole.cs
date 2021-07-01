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
    /// 账号角色关系
    /// </summary>
    [Table("EMS_Sys_AccountRole")]
    public class AccountRole : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        [NotNull]
        [Required]
        public Guid AccountId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [NotNull]
        [Required]
        public Guid RoleId { get; set; }
    }
}
