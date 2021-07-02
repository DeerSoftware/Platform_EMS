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

        public AccountRole() { }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="accountId">账号Id</param>
        /// <param name="roleId">角色id</param>
        public AccountRole(Guid id, Guid accountId, Guid roleId)
        {
            this.Id = id;
            this.AccountId = accountId;
            this.RoleId = roleId;
        }
    }
}
