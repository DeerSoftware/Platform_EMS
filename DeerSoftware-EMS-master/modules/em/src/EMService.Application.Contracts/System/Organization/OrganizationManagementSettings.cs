using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 组织查询、分页配置
    /// </summary>
    public static class OrganizationManagementSettings
    {
        public const string GroupName = "OrganizationManagement";

        /// <summary>
        /// Maximum allowed page size for paged list requests.
        /// </summary>
        public const string MaxPageSize = GroupName + ".MaxPageSize";
    }
}
