using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 组织树
    /// </summary>
    public class OrganizationTreeDto
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string lable { get; set; }
        /// <summary>
        /// 组织Id
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public int parentId { get; set; }
        /// <summary>
        /// 组织类型
        /// </summary>
        public OrganizationType Type { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        public List<OrganizationTreeDto> children { get; set; }
    }
}
