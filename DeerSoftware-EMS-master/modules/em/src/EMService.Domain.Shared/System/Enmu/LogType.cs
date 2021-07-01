using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login = 1,
        /// <summary>
        /// 访问
        /// </summary>
        Visit = 2,
        /// <summary>
        /// 操作
        /// </summary>
        Operation = 3,
        /// <summary>
        /// 异常
        /// </summary>
        Error = 4,
    }
}
