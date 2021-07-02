using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 统一返回数据对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ResultType ResultType { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
