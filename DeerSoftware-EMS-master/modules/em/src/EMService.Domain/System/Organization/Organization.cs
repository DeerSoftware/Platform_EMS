using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EMService
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [Table("EMS_Sys_Organization")]
    public class Organization : FullAuditedAggregateRoot<int>
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        [NotNull]
        [Required]
        public string OrgName { get; set; }
        /// <summary>
        /// 组织昵称
        /// </summary>
        public string OrgNickName { get; set; }
        /// <summary>
        /// 所属上级
        /// </summary>
        [Required]
        [NotNull]
        public int ParentId { get; set; }
        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 分机号
        /// </summary>
        public string PhoneExt { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public Guid ResponsiblePerson { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 所属层
        /// </summary>
        [NotNull]
        public string Level { get; set; }
        #region 构造函数
        public Organization() { }
        internal Organization(
            int Id,
            string OrgName,
            string OrgNickName,
            int ParentId,
            string OrgCode,
            string Phone,
            string PhoneExt,
            string Email,
            int Sort,
            Guid ResponsiblePerson,
            string Address,
            string Remark
            )
        {
            Check.NotNullOrWhiteSpace(OrgName, nameof(OrgName));
        }
        #endregion 
    }
}
