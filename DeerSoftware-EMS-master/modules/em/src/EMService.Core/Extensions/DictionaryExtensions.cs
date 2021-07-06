using EMService.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMService.Core.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 获取键的值，缺少键返回默认值，键值转换目标类型错误返回默认值，引用类型的null不会返回dft
        /// </summary>
        public static T GetOrDefault<T>(this IDictionary<string, object> dic, string key, T dft = default(T))
        {
            if (dic == null)
            {
                throw new Exception($"参数字典不能为空");
            }

            if (dic.ContainsKey(key))
            {
                try
                {
                    return ConvertUtil.To<T>(dic[key]);
                }
                catch
                {
                    return dft;
                }
            }
            return dft;
        }

        /// <summary>
        /// 获取键的值，缺少键报错，键值转换目标类型报错，引用类型的null不报错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetOrError<T>(this IDictionary<string, object> dic, string key)
        {
            if (dic == null)
            {
                throw new Exception($"参数字典不能为空");
            }

            if (dic.ContainsKey(key))
            {
                try
                {
                    return ConvertUtil.To<T>(dic[key]);
                }
                catch (Exception ex)
                {
                    throw new Exception($"参数中存在无法转换的键值 {key}", ex);
                }
            }
            throw new Exception($"参数字典中不包含键 {key}");
        }
    }
}
