using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 激活
        /// </summary>
        Activate = 1,
        /// <summary>
        /// 冻结
        /// </summary>
        Blocking = 2
    }
}
