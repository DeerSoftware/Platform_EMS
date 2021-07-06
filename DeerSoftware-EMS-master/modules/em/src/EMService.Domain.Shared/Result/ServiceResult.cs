using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService.Result
{
    /// <summary>
    /// 服务层响应实体
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public ServiceResultCode Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public bool Success => Code == ServiceResultCode.Succeed;

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void IsSuccess()
        {
            Message = ServiceResultCode.Succeed.GetDescription();
            Code = ServiceResultCode.Succeed;
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void IsFailed(ServiceResultCode serviceResultCode, Exception ex = null)
        {
            Message = serviceResultCode.GetDescription();
            Code = serviceResultCode;

            //日志逻辑
            if (ex != null)
            {

            }
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="serviceResultCode"></param>
        public void IsWarning(ServiceResultCode serviceResultCode)
        {
            Message = serviceResultCode.GetDescription();
            Code = serviceResultCode;
        }
    }

    /// <summary>
    /// 服务层响应实体(泛型)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T> : ServiceResult where T : class
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public void IsSuccess(T data = null)
        {
            Message = ServiceResultCode.Succeed.GetDescription();
            Code = ServiceResultCode.Succeed;
            Data = data;
        }
       
    }

}
