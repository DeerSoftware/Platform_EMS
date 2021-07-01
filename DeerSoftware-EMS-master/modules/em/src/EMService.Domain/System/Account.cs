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
    /// 账号实体
    /// </summary>
    [Table("EMS_Sys_Account")]
    public class Account : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 用户名
        ///// </summary>
        [NotNull]
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [NotNull]
        [Required] 
        public string RealName { get; set; }
        /// <summary>
        /// 员工编码
        /// </summary>
        [NotNull]
        [Required] 
        public string UserCode { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [NotNull]
        [Required] 
        public Guid RoleId { get; set; }
        /// <summary>
        /// 组织Id
        /// </summary>
        [NotNull]
        [Required]
        public Guid OrgId { get; set; }


        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public Status Status  { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
