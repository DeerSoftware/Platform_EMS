using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EMService.Result
{
    public static class ResultExtensions
    {
        /// <summary>
        /// 获取到对应枚举的描述-没有描述信息，返回枚举值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            string name = Enum.GetName(type, @enum);

            if (name == null)
                return null;
            FieldInfo field = type.GetField(name);

            if (!(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute))
                return name;
            return attribute?.Description;
        }

        public static ServiceResult<T> ServiceResultSuccess<T>(this Enum @enum, T data)
            where T : class
        {
            ServiceResult<T> result = new ServiceResult<T>();
            result.IsSuccess(data);

            return result;
        }

        public static ServiceResult<T> ServiceResultFailed<T>(this Enum @enum, Exception ex = null)
            where T : class
        {
            ServiceResult<T> result = new ServiceResult<T>();
            result.IsFailed((ServiceResultCode)@enum, ex);

            return result;
        }
    }
}
