using System;
using Newtonsoft.Json;
using System.ComponentModel;
using EMService.Core.Extensions;

namespace EMService.Core.Common
{
    public static class ConvertUtil
    {
        public static T To<T>(object obj)
        {
            if (obj is T)
                return (T)obj;

            var type = typeof(T);

            if (obj == null)
            {
                if (!type.IsValueType)
                    return default(T);

                throw new InvalidCastException($"无法将null转换成类型 {type.FullName}");
            }
            try
            {
                return (T)obj;
            }
            catch (Exception ex)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(obj.ToString());
                }
                catch (Exception nb)
                {
                    var stype = obj.GetType();
                    var cvt = TypeDescriptor.GetConverter(type);

                    if (cvt.CanConvertFrom(stype))
                        return (T)cvt.ConvertFrom(obj);

                    var scvt = TypeDescriptor.GetConverter(stype);

                    if (scvt.CanConvertTo(type))
                        return (T)scvt.ConvertTo(obj, type);
                    
                    throw new InvalidCastException($"无法将类型 {stype.FullName} 转换成类型 {type.FullName},{nb.AllMessage()}", ex);
                }
            }
        }
        public static object To(object obj, Type type)
        {
            if (obj == null)
            {
                if (!type.IsValueType)
                    return null;

                throw new InvalidCastException($"无法将null转换成类型 {type.FullName}");
            }
            try
            {
                if (obj.GetType() == type)
                    return obj;

                return Convert.ChangeType(obj, type);
            }
            catch (Exception ex)
            {
                try
                {
                    return JsonConvert.DeserializeObject(obj.ToString(), type);
                }
                catch (Exception)
                {
                    var stype = obj.GetType();
                    var cvt = TypeDescriptor.GetConverter(type);

                    if (cvt.CanConvertFrom(stype))
                        return cvt.ConvertFrom(obj);

                    var scvt = TypeDescriptor.GetConverter(stype);

                    if (scvt.CanConvertTo(type))
                        return scvt.ConvertTo(obj, type);

                    throw new InvalidCastException($"无法将类型 {stype.FullName} 转换成类型 {type.FullName}", ex);
                }
            }
        }
    }
}
