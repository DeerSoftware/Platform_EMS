using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 常量设置基类
    /// </summary>
    public abstract class Const
    {
        /// <summary>
        /// 编码最大长度
        /// </summary>
        public const int MaxCodeLength = 32;
        /// <summary>
        /// 名称最大长度
        /// </summary>
        public const int MaxNameLength = 256;
        /// <summary>
        /// 备注最大长度
        /// </summary>
        public const int MaxRemarkLength = 128;
        /// <summary>
        /// 手机号最大长度
        /// </summary>
        public const int MaxMobileLength = 11;
        /// <summary>
        /// 坐机号最大长度，如：0551-66703427
        /// </summary>
        public const int MaxPhoneLength = 13;
        /// <summary>
        /// 坐机分机号最大长度 ，如：4536
        /// </summary>
        public const int MaxPhoneExtLength = 4;
        /// <summary>
        /// 电子邮箱地址最大长度
        /// </summary>
        public const int MaxEmailLength = 64;
    }
}
