using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alabo.Helpers
{
    public static class StringHelper
    {
        #region 截取字符串的后部分

        /// <summary>
        ///     截取字符串中间的部分
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="beforeValue">开始拆分字符串</param>
        /// <param name="afterValue">结束拆分字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string SubstringBetween(this string source, string beforeValue, string afterValue)
        {
            if (string.IsNullOrEmpty(beforeValue)) return source;

            if (string.IsNullOrEmpty(afterValue)) return source;

            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            var index = compareInfo.IndexOf(source, beforeValue, CompareOptions.IgnoreCase);
            if (index < 0) return source;

            var lastIndex = compareInfo.IndexOf(source, afterValue, index, CompareOptions.IgnoreCase);
            if (lastIndex < 0) return source;

            if (lastIndex < index + beforeValue.Length) return source;

            source = source.Substring(index + beforeValue.Length, lastIndex - index - beforeValue.Length);
            return source;
        }

        #endregion 截取字符串的后部分

        #region 截取字符串的后部分

        /// <summary>
        ///     截取字符串的后部分
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="value">拆分字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string SubstringAfter(this string source, string value)
        {
            if (string.IsNullOrEmpty(value)) return source;

            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            var index = compareInfo.IndexOf(source, value, CompareOptions.Ordinal);
            if (index < 0) return string.Empty;

            return source.Substring(index + value.Length);
        }

        #endregion 截取字符串的后部分

        #region 截取字符串的前部分

        /// <summary>
        ///     截取字符串的前部分
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="value">拆分字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string SubstringBefore(this string source, string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            var index = compareInfo.IndexOf(source, value, CompareOptions.Ordinal);
            if (index < 0) return string.Empty;

            return source.Substring(0, index);
        }

        #endregion 截取字符串的前部分

        #region 追加字符串，用分隔符分隔，默认分隔符为“,”

        /// <summary>
        ///     追加字符串，用分隔符分隔，默认分隔符为“,”
        /// </summary>
        /// <param name="sb">StringBulider对象</param>
        /// <param name="append">要追加的字符串</param>
        /// <param name="split">分隔符</param>
        public static void AppendString(this StringBuilder sb, string append, string split = ",")
        {
            if (sb.Length == 0)
            {
                sb.Append(append);
                return;
            }

            sb.Append(split);
            sb.Append(append);
        }

        #endregion 追加字符串，用分隔符分隔，默认分隔符为“,”

        #region 替换所有HTML标签为空

        /// <summary>
        ///     替换所有HTML标签为空
        /// </summary>
        /// <param name="input">The string whose values should be replaced.</param>
        /// <returns>A string.</returns>
        public static string RemoveHtml(this string input)
        {
            var stripTags = new Regex("</?[a-z][^<>]*>", RegexOptions.IgnoreCase);
            return stripTags.Replace(input, string.Empty);
        }

        #endregion 替换所有HTML标签为空

        #region 解码网页参数中的字符串

        /// <summary>
        ///     解码网页参数中的字符串
        /// </summary>
        public static string InputTexts(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            text = Regex.Replace(text, @"[\s]{2,}", " ");
            text = Regex.Replace(text, @"(<[b|B][r|R]/*>)+|(<[p|P](.|\n)*?>)", "\n");
            text = Regex.Replace(text, @"(\s*&[n|N][b|B][s|S][p|P];\s*)+", " ");
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            text = text.Replace("'", "''");
            return text;
        }

        #endregion 解码网页参数中的字符串

        #region 把string转换为double

        /// <summary>
        ///     把string转换为double
        /// </summary>
        public static double String2Double(string str)
        {
            if (!double.TryParse((str ?? string.Empty).Trim(), out var result)) return -1;

            return result;
        }

        #endregion 把string转换为double

        #region 把string转换为decimal

        /// <summary>
        ///     把string转换为decimal
        /// </summary>
        public static decimal StringToDecimal(string str, decimal defaultValue = -1M)
        {
            if (!decimal.TryParse((str ?? string.Empty).Trim(), out var result)) return defaultValue;

            return result;
        }

        #endregion 把string转换为decimal

        #region 把string转换为DateTime

        /// <summary>
        ///     把string转换为DateTime
        /// </summary>
        public static DateTime StringToDateTime(string str)
        {
            if (!DateTime.TryParse((str ?? string.Empty).Trim(), out var result)) return new DateTime(1970, 1, 1);

            return result;
        }

        #endregion 把string转换为DateTime

        #region 把string转换为int

        /// <summary>
        ///     把string转换为int
        /// </summary>
        public static int StringToInt(string str, int defaultValue = -1)
        {
            if (!int.TryParse((str ?? string.Empty).Trim(), out var result)) return defaultValue;

            return result;
        }

        #endregion 把string转换为int

        #region 把对象转换为字符串，如对象为null则返回空字符串

        /// <summary>
        ///     把对象转换为字符串，如对象为null则返回空字符串
        /// </summary>
        public static string ToString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        #endregion 把对象转换为字符串，如对象为null则返回空字符串

        #region 尝试转换为int，如果失败返回null

        /// <summary>
        ///     尝试转换为int，如果失败返回null
        /// </summary>
        public static int? TryToInt(object data)
        {
            if (!int.TryParse(data.ToString(), out var v)) return null;

            return v;
        }

        #endregion 尝试转换为int，如果失败返回null

        #region 转换由逗号隔开的数字组字符串到数字列表

        /// <summary>
        ///     转换由逗号隔开的数字组字符串到数字列表
        ///     例
        ///     "1,2,3" -> List<int>(){1, 2, 3}
        /// </summary>
        public static List<int> StringToIntList(string data)
        {
            //如果字符串为null则返回空列表
            if (data == null) return new List<int>();
            //转换
            var result = (from s in data.Split(',')
                let v = TryToInt(s)
                where v.HasValue // v != null
                select v.Value).ToList();
            return result;
        }

        #endregion 转换由逗号隔开的数字组字符串到数字列表

        #region 转换数字列表到由逗号隔开的数字组字符串

        /// <summary>
        ///     转换数字列表到由逗号隔开的数字组字符串
        ///     例
        ///     List<int>(){1, 2, 3} -> "1,2,3"
        /// </summary>
        public static string IntListToString(IEnumerable<int> intList)
        {
            //如果列表为null则返回空字符串
            if (intList == null) return string.Empty;
            //转换
            var result = string.Join(",", from v in intList select v.ToString());
            return result;
        }

        #endregion 转换数字列表到由逗号隔开的数字组字符串

        #region 尝试转换为uint，如果失败返回null

        /// <summary>
        ///     尝试转换为uint，如果失败返回null
        /// </summary>
        public static uint? TryToUInt(object data)
        {
            if (!uint.TryParse(data.ToString(), out var v)) return null;

            return v;
        }

        #endregion 尝试转换为uint，如果失败返回null

        #region 转换由逗号隔开的数字组字符串到无符号数字列表

        /// <summary>
        ///     转换由逗号隔开的数字组字符串到无符号数字列表
        ///     例
        ///     "1,2,3" -> List uint(){1, 2, 3}
        /// </summary>
        public static List<uint> StringToUIntList(string data)
        {
            //如果字符串为null则返回空列表
            if (data == null) return new List<uint>();
            //转换
            var result = (from s in data.Split(',')
                let v = TryToUInt(s)
                where v.HasValue // v != null
                select v.Value).ToList();
            return result;
        }

        #endregion 转换由逗号隔开的数字组字符串到无符号数字列表

        #region 转换无符号数字列表到由逗号隔开的数字组字符串

        /// <summary>
        ///     转换无符号数字列表到由逗号隔开的数字组字符串
        ///     例
        ///     List<uint>(){1, 2, 3} -> "1,2,3"
        /// </summary>
        public static string UIntListToString(IEnumerable<uint> uintList)
        {
            //如果列表为null则返回空字符串
            if (uintList == null) return string.Empty;
            //转换
            var result = string.Join(",", from v in uintList select v.ToString());
            return result;
        }

        #endregion 转换无符号数字列表到由逗号隔开的数字组字符串

        #region 获取格式化过的小数字符串，retain代表保留的小数点位数

        /// <summary>
        ///     获取格式化过的小数字符串，retain代表保留的小数点位数
        ///     这个函数使用的是四舍五入
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <param name="retain">要保留的小数点位数</param>
        /// <returns>格式化过的小数字符串</returns>
        public static string GetDecimalString(object obj, int retain)
        {
            var dec = obj as decimal?;
            if (dec == null) dec = StringToDecimal(obj.ToString());

            dec = Math.Round(dec.Value, retain, MidpointRounding.AwayFromZero);
            var str = string.Format("{0:0." + new string('0', retain) + "}", dec);
            return str;
        }

        #endregion 获取格式化过的小数字符串，retain代表保留的小数点位数

        #region 通配符转换到正则

        /// <summary>
        ///     通配符转换到正则
        /// </summary>
        public static Regex WillcardToRegex(string willcard)
        {
            return new Regex(
                "^" + Regex.Escape(willcard).Replace(@"\*", ".*").Replace(@"\?", ".") +
                "$",
                RegexOptions.IgnoreCase | RegexOptions.Singleline
            );
        }

        #endregion 通配符转换到正则

        #region 客户端用的函数

        public static string Left(string str, int len)
        {
            if (str.Length > len) return str.Substring(0, len);

            return str + "...";
        }

        public static string[] SplitString(string source, string delimiter)
        {
            int iPos, iLength;
            bool bEnd;
            string sTemp;
            IList<string> list = new List<string>();

            sTemp = source;
            iLength = delimiter.Length;
            bEnd = sTemp.EndsWith(delimiter);
            for (;;)
            {
                iPos = sTemp.IndexOf(delimiter);
                if (iPos < 0) break;

                list.Add(sTemp.Substring(0, iPos));
                sTemp = sTemp.Substring(iPos + iLength);
            }

            if (sTemp.Length >= 0 || bEnd) list.Add(sTemp);

            var array = new string[list.Count];
            var k = 0;
            foreach (var item in list)
            {
                array[k] = item;
                k++;
            }

            return array;
        }

        public static IList<string> SplitStringList(string source, string delimiter)
        {
            IList<string> list = new List<string>();
            var array = SplitString(source, delimiter);

            foreach (var item in array) list.Add(item);

            return list;
        }

        public static char[] NumberArray()
        {
            var str = "0123456789abcdefghijklmnopqrstuvwxyz.";
            return str.ToCharArray();
        }

        /// <summary>
        ///     根据分隔符，将IList转换成String
        /// </summary>
        /// <param name="list"></param>
        /// <param name="split"></param>
        public static string ConvertIListToString(IList<string> list, string split)
        {
            var str = string.Empty;
            var k = 0;
            foreach (var item in list)
            {
                if (k < list.Count - 1)
                    str += item + split;
                else
                    str += item;

                k++;
            }

            return str;
        }

        /// <summary>
        ///     去掉连续重复三个的字符
        /// </summary>
        /// <param name="str"></param>
        public static string ClearRepeatChar(string str)
        {
            var temp = str;
            for (var i = 0; i < str.Length; i++)
                try
                {
                    var item = str.Substring(i, 1);
                    if (item != ".")
                    {
                        var partter = item + "{3,}";
                        str = Regex.Replace(str, partter, item);
                    }
                }
                catch
                {
                }

            return str;
        }

        /// <summary>
        ///     将Object转换成String
        /// </summary>
        /// <param name="str"></param>
        public static string ObjectToString(object str)
        {
            if (str == null) return string.Empty;

            return str.ToString();
        }

        #endregion 客户端用的函数
    }
}