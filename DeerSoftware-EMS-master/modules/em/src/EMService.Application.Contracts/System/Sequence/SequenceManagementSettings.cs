using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 序列查询、分页配置
    /// </summary>
    public static class SequenceManagementSettings
    {
        public const string GroupName = "SequenceManagement";

        /// <summary>
        /// Maximum allowed page size for paged list requests.
        /// </summary>
        public const string MaxPageSize = GroupName + ".MaxPageSize";
    }
}
