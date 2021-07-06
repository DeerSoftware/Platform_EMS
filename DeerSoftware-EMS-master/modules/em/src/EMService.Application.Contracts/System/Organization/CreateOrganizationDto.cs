using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 组强新增Dto
    /// </summary>
    public class CreateOrganizationDto
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 组织昵称
        /// </summary>
        public string OrgNickName { get; set; }
        /// <summary>
        /// 所属上级
        /// </summary>
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
        /// 当前所属
        /// </summary>
        public string Level { get; set; }
    }
}
