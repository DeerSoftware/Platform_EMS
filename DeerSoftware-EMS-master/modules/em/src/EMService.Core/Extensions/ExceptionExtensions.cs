using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService.Core.Extensions
{
    /// <summary>
    /// 异常扩展方法
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 返回异常的具体信息，如果异常包含内部异常，会将内部异常的信息一起展示出来，
        /// 如果异常时聚合异常，会将内部所有异常的信息展示出来
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string AllMessage(this System.Exception ex)
        {
            if (ex is AggregateException)
            {
                var aex = ex as AggregateException;
                var info = string.Join(Environment.NewLine, aex.InnerExceptions.Select(AllMessage).Distinct());
                return info;
            }
            var list = new List<string>();
            while (ex != null)
            {
                if (ex.InnerException == null)
                {
                    list.Add(ex.Message);
                }
                ex = ex.InnerException;
            }
            return string.Join(Environment.NewLine, list.Distinct());
        }
    }
}
