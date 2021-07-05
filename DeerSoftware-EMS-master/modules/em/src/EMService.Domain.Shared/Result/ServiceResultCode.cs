using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService.Result
{
    /// <summary>
    /// 服务层响应码枚举
    /// </summary>
    public enum ServiceResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Succeed = 200,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("请求失败")]
        Failed = -1,
    }
}
