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
        /// 组织类型
        /// </summary>
        [Required]
        [NotNull] 
        public OrganizationType OrganizationType { get; set; }
        /// <summary>
        /// 所属层
        /// </summary>
        [NotNull]
        public string Level { get; set; }

        #region 函数
        public Organization SetOrgName([NotNull] string orgName)
        {
            Check.NotNullOrWhiteSpace(orgName, nameof(orgName));

            //if (name.Length >= OrganizationConsts.MaxNameLength)
            //{
            //    throw new ArgumentException($"Organization OrgName can not be longer than {OrganizationConsts.MaxNameLength}");
            //}
            OrgName = orgName;
            return this;
        }
        public Organization SetParentId([NotNull] int parentId)
        {
            Check.NotNull<int>(parentId, nameof(parentId));

            //if (name.Length >= OrganizationConsts.MaxNameLength)
            //{
            //    throw new ArgumentException($"Organization OrgName can not be longer than {OrganizationConsts.MaxNameLength}");
            //}
            ParentId = parentId;
            return this;
        }
        public Organization SetLevel([NotNull] string level)
        {
            Check.NotNullOrWhiteSpace(level, nameof(level));

            //if (name.Length >= OrganizationConsts.MaxNameLength)
            //{
            //    throw new ArgumentException($"Organization OrgName can not be longer than {OrganizationConsts.MaxNameLength}");
            //}
            Level = level;
            return this;
        }

        #endregion


        #region 构造函数
        public Organization() { }
        internal Organization(
            int id,
            string orgName,
            string orgNickName,
            int parentId,
            string orgCode,
            string phone,
            string phoneExt,
            string email,
            int sort,
            Guid responsiblePerson,
            string address,
            string remark,
            string level
            )
        {
            this.Id = id;
            SetOrgName(Check.NotNullOrWhiteSpace(orgName, nameof(orgName)));
            this.OrgNickName = orgNickName;
            SetParentId(Check.NotNull<int>(parentId, nameof(parentId)));
            this.OrgCode = orgCode;
            this.Phone = phone;
            this.PhoneExt = phoneExt;
            this.Email = email;
            this.Sort = sort;
            this.ResponsiblePerson = responsiblePerson;
            this.Address = address;
            this.Remark = remark;
            SetLevel(Check.NotNullOrWhiteSpace(level, nameof(level)));
            //附加
            this.CreatorId = Guid.NewGuid();
        }
        #endregion 
    }
}
