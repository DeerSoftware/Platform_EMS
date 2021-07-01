using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService
{
    /// <summary>
    /// 账号角色关系
    /// </summary>
    public class AccountRole : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
