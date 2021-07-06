using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;

namespace EMService.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex space = new Regex(@"\s+");

        #region 字符串扩展

        /// <summary>
        /// 判断字符串是空或空字串或空白字串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;

            return false;
        }

        /// <summary>
        /// 判断字符串不是空或空字串或空白字串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmptyOrWhiteSpace(this string value)
        {
            return !value.IsNullOrEmptyOrWhiteSpace();
        }

        #endregion

        #region 全角转换半角以及半角转换为全角

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSbc(this string input)
        {
            if (input.IsNullOrEmptyOrWhiteSpace())
                return input;
            // 半角转全角：
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127)
                {
                    array[i] = (char)(array[i] + 65248);
                }
            }
            return new string(array);
        }

        /// <summary>
        /// 转半角的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDbc(this string input)
        {
            if (input.IsNullOrEmptyOrWhiteSpace())
                return input;
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }

        /// <summary>
        /// 清除字串中的回车符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveEnterKey(this string input)
        {
            return input?.Replace("\n", "").Replace("\r", "");
        }

        /// <summary>
        /// 将字串中多个连续空格替换成一个空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveMoreSpace(this string input)
        {
            if (input == null)
                return null;

            return space.Replace(input, " ");
        }
        #endregion

        /// <summary>
        /// 取一个字串的左侧指定长度的子字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            Check.NotNullOrEmpty(value, nameof(value));

            var len = value.Length;
            if (len <= length)
            {
                return value;
            }
            return value.Substring(0, length);
        }
        /// <summary>
        /// 取一个字串的右侧指定长度的子字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            Check.NotNullOrEmpty(value, nameof(value));

            var len = value.Length;
            if (len <= length)
            {
                return value;
            }
            return value.Substring(len - length);
        }
        /// <summary>
        /// 取一个字串的去除左侧指定长度的子字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RemoveLeft(this string value, int length)
        {
            Check.NotNullOrEmpty(value, nameof(value));

            var len = value.Length;
            if (len <= length)
            {
                return value;
            }
            return value.Right(value.Length - length);
        }
        /// <summary>
        /// 取一个字串的去除右侧指定长度的子字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RemoveRight(this string value, int length)
        {
            Check.NotNullOrEmpty(value, nameof(value));

            var len = value.Length;
            if (len <= length)
            {
                return value;
            }
            return value.Left(value.Length - length);
        }

        /// <summary>
        /// 根据区域大小写和排序规则判断源字符串是否包含目标字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            Check.NotNull(source, nameof(source));
            return source.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// 将文本超出长度的部分换成省略号，或直接截取
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static string Ellipse(this string source, int length, char ellipse = '.')
        {
            if (source.IsNullOrEmptyOrWhiteSpace())
                return source;

            if (source.Length > length)
            {
                if (length > 3)
                {
                    return source.Left(length - 3) + new string(ellipse, 3);
                }

                return source.Left(length);
            }

            return source;
        }
    }
}
