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
        public int OrgId { get; set; }
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
        public Status Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public Account() { }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="userName">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="userCode">员工编码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="orgId">组织Id</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="position">职位</param>
        /// <param name="telephone">电话</param>
        /// <param name="gender">性别</param>
        /// <param name="status">账号状态</param>
        /// <param name="remark">备注</param>
        public Account(
            Guid id,
            string userName,
            string realName,
            string userCode,
            string mobile,
            string password,
            Guid roleId,
            int orgId,
            string email,
            string position,
            string telephone,
            int gender,
            Status status,
            string remark
            )
        {
            this.Id = id;
            this.UserName = userName;
            this.RealName = realName;
            this.UserCode = userCode;
            this.Mobile = mobile;
            this.Password = password;
            this.RoleId = roleId;
            this.OrgId = orgId;
            this.Email = email;
            this.Position = position;
            this.Telephone = telephone;
            this.Gender = gender;
            this.Status = status;
            this.Remark = remark;
        }
    }
}
