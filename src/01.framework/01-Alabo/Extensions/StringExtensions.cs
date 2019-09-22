using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Extensions {

    /// <summary>
    ///     文本类型数据扩展
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        ///     空白字符串的定义
        /// </summary>
        private static readonly char[] BlankChars = { ' ', '\t', '\r', '\n', '\b' };

        /// <summary>
        ///     空白列表
        /// </summary>
        private static readonly string[] SpaceList = { " ", "　", "\r", "\n", "\t" };

        /// <summary>
        ///     [2**n for n in xrange(8)]
        /// </summary>
        private static readonly byte[] TrueByteMap = { 1, 2, 4, 8, 16, 32, 64, 128 };

        /// <summary>
        ///     [~(2**n)&amp;0xff for n in xrange(8)]
        /// </summary>
        private static readonly byte[] FalseByteMap = { 254, 253, 251, 247, 239, 223, 191, 127 };

        internal static string SafeStringJoin(IEnumerable<string> enumerable) {
            throw new NotImplementedException();
        }

        #region 分析url链接，返回参数集合

        /// <summary>
        ///     分析url链接，返回参数集合
        ///     将查询字符串解析转换为名值集合
        /// </summary>
        /// <param name="url">url链接</param>
        /// <param name="baseUrl"></param>
        public static NameValueCollection ParseUrl(string url, out string baseUrl) {
            baseUrl = "";
            if (string.IsNullOrEmpty(url)) {
                return null;
            }

            var nvc = new NameValueCollection();

            try {
                var questionMarkIndex = url.IndexOf('?');

                if (questionMarkIndex == -1) {
                    baseUrl = url;
                } else {
                    baseUrl = url.Substring(0, questionMarkIndex);
                }

                if (questionMarkIndex == url.Length - 1) {
                    return null;
                }

                var ps = url.Substring(questionMarkIndex + 1);

                // 开始分析参数对
                var re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
                var mc = re.Matches(ps);

                foreach (Match m in mc) {
                    nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
                }
            } catch {
            }

            return nvc;
        }

        #endregion 分析url链接，返回参数集合

        #region Join(将集合连接为带分隔符的字符串)

        /// <summary>
        ///     将集合连接为带分隔符的字符串
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(IEnumerable<T> list, string quotes = "", string separator = ",") {
            if (list == null) {
                return string.Empty;
            }

            var result = new StringBuilder();
            foreach (var each in list) {
                result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
            }

            if (separator == "") {
                return result.ToString();
            }

            return result.ToString().TrimEnd(separator.ToCharArray());
        }

        #endregion Join(将集合连接为带分隔符的字符串)

        /// <summary>
        ///     安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        //public static string SafeString(this object input) {
        //    return input == null ? string.Empty : input.ToString().Trim();
        //}

        /// <summary>
        ///     获取最大长度的字符串
        ///     字符串长度超过最大长度时截取字符串并在后面添加后缀
        ///     后缀suffix的默认值是"..."
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">后缀，默认值是"..."</param>
        public static string LeftString(
            this string str, int maxLength, string suffix = null) {
            if (str.IsNullOrEmpty()) {
                return str;
            }

            if (str.Length > maxLength) {
                str = str.Substring(0, maxLength) + (suffix ?? string.Empty);
            }

            return str;
        }

        /// <summary>
        ///     比较两个字符串的大小，不区分大小写
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareString"></param>
        /// <returns></returns>
        public static bool Equal(this string input, string compareString) {
            if (input.IsNullOrEmpty()) {
                if (compareString.IsNullOrEmpty()) {
                    return true;
                }

                return false;
            }

            return input.Trim().Equals(compareString.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     根据分隔符转出数组
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter.</param>
        public static List<string> ToSplitList(this object source, string delimiter = ",") {
            var list = new List<string>();
            var array = SplitString(source, delimiter);

            foreach (var item in array) {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        ///     Splits the list.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="splitChar">The split character.</param>
        public static List<string> SplitList(this string source, char[] splitChar) {
            return source.Split(splitChar, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        ///     通过,将字符串分割成Guid List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<Guid> SliptToGuidList(this string input) {
            var list = input.SplitList(new[] { ',' });
            var resultList = new List<Guid>();
            foreach (var item in list) {
                var guid = item.ToGuid();
                resultList.Add(guid);
            }

            return resultList;
        }

        /// <summary>
        ///     转换为Mongodb的ObjectId
        /// </summary>
        /// <param name="str"></param>
        public static ObjectId ToObjectId(this string str) {
            if (str.IsNullOrEmpty()) {
                return ObjectId.Empty;
            }

            if (str.Length != 24) {
                return ObjectId.Empty;
            }

            try {
                return ObjectId.Parse(str);
            } catch (Exception ex) {
                throw new ArgumentException("转换出错" + ex.Message);
            }
        }

        /// <summary>
        ///     转换为Mongodb的ObjectId
        ///     如果出错时候，直接抛出异常
        /// </summary>
        /// <param name="str"></param>
        public static ObjectId ToSafeObjectId(this string str) {
            try {
                return ObjectId.Parse(str);
            } catch (Exception ex) {
                throw new ArgumentException("转换出错" + ex.Message);
            }
        }

        public static List<string> GetEnumMemberList(this Enum insEnum) {
            var rs = new List<string>();

            return rs;
        }

        /// <summary>
        ///     字符串不是空
        /// </summary>
        /// <param name="str">字符串</param>
        public static bool IsNotNullOrEmpty(this object str) {
            return !str.IsNullOrEmpty();
        }

        /// <summary>
        ///     字符串是否为NUL或者为空
        /// </summary>
        /// <param name="str">字符串</param>
        public static bool IsNullOrEmpty(this string str) {
            if (str == null) {
                return true;
            }

            if (!string.IsNullOrEmpty(str.Trim())) {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     字符串是否为NUL或者为空
        /// </summary>
        /// <param name="str">字符串</param>
        public static bool IsNullOrEmpty(this object str) {
            if (str == null) {
                return true;
            }

            if (!string.IsNullOrEmpty(str.ToString())) {
                return false;
            }

            return true;
        }

        public static bool IsNullOrEmpty(this ObjectId objectId) {
            if (objectId == ObjectId.Empty) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Determines whether [is unique identifier null 或 empty] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        public static bool IsGuidNullOrEmpty(this object str) {
            if (str.IsNullOrEmpty()) {
                return true;
            }

            if (str.ToString().Length != 36) {
                return true;
            }

            if (str.ToString() == "00000000-0000-0000-0000-000000000000") {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     判断OjbectId是否为空，或null
        /// </summary>
        /// <param name="str"></param>
        public static bool IsObjectIdNullOrEmpty(this object str) {
            if (str.IsNullOrEmpty()) {
                return true;
            }

            if (str.ToString().Length != 24) {
                return true;
            }

            if (str.ToStr().ToObjectId() == ObjectId.Empty) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Splits the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter.</param>
        public static string[] SplitString(this object source, string delimiter = ",") {
            int iPos, iLength;
            bool bEnd;
            string sTemp;
            IList<string> list = new List<string>();

            if (source.IsNullOrEmpty()) {
                return list.ToArray();
            }

            sTemp = source.ToString();
            iLength = delimiter.Length;
            bEnd = sTemp.EndsWith(delimiter);
            for (; ; )
            {
                iPos = sTemp.IndexOf(delimiter);
                if (iPos < 0) {
                    break;
                }

                list.Add(sTemp.Substring(0, iPos));
                sTemp = sTemp.Substring(iPos + iLength);
            }

            if (sTemp.Length >= 0 || bEnd) {
                list.Add(sTemp);
            }

            var array = new string[list.Count];
            var k = 0;
            foreach (var item in list) {
                array[k] = item;
                k++;
            }

            return array;
        }

        /// <summary>
        ///     将字符串反序列化成T类型
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="t">The t.</param>
        public static List<T> Deserialize<T>(this string str, T t) {
            var js = new JsonSerializer();
            if (str.IsNullOrEmpty()) {
                return null;
            }

            var obj = js.Deserialize(new JsonTextReader(new StringReader(str)), typeof(List<T>));
            if (obj == null) {
                return default;
            }

            return (List<T>)obj;
        }

        /// <summary>
        ///     将字符串反序列化成T类型
        /// </summary>
        /// <param name="str">The string.</param>
        public static List<T> Deserialize<T>(this string str) {
            if (str == null) {
                return null;
            }

            var js = new JsonSerializer();
            var obj = js.Deserialize(new JsonTextReader(new StringReader(str)), typeof(List<T>));
            if (obj == null) {
                return new List<T>();
            }

            return (List<T>)obj;
        }

        /// <summary>
        ///     判断是否能转型成功
        /// </summary>
        /// <param name="str">The string.</param>
        public static bool IsDeserialize<T>(this string str) {
            var isDeserialize = true;
            try {
                JsonConvert.DeserializeObject<T>(str);
            } catch (Exception) {
                isDeserialize = false;
            }

            return isDeserialize;
        }

        /// <summary>
        ///     转换成时间
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="throwExceptionIfFailed">if set to <c>true</c> [throw exception if failed].</param>
        public static DateTime ToDate(this string input, bool throwExceptionIfFailed = false) {
            var valid = DateTime.TryParse(input, out var result);
            if (!valid) {
                if (throwExceptionIfFailed) {
                    throw new FormatException(string.Format("'{0}' cannot be converted as DateTime", input));
                }
            }

            return result;
        }

        /// <summary>
        ///     Replaces the regex.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="oldString">The old string.</param>
        /// <param name="newString">The new string.</param>
        public static string ReplaceRegex(this string input, string oldString, string newString) {
            input.Replace(oldString, newString);
            var regex = new Regex(oldString);
            regex.Replace(oldString, newString);
            return string.Empty;
        }

        /// <summary>
        ///     Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="throwExceptionIfFailed">if set to <c>true</c> [throw exception if failed].</param>
        public static int ToInt(this string input, bool throwExceptionIfFailed = false) {
            var match = Regex.Match(input, @"^[0-9]+$", RegexOptions.IgnoreCase);
            if (match.Success) {
                return Convert.ToInt32(input);
            }

            return 0;
        }

        /// <summary>
        ///     To the double.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="throwExceptionIfFailed">if set to <c>true</c> [throw exception if failed].</param>
        public static double ToDouble(this string input, bool throwExceptionIfFailed = false) {
            var valid = double.TryParse(input, NumberStyles.AllowDecimalPoint,
                new NumberFormatInfo { NumberDecimalSeparator = "." }, out var result);
            if (!valid) {
                if (throwExceptionIfFailed) {
                    throw new FormatException(string.Format("'{0}' cannot be converted as double", input));
                }
            }

            return result;
        }

        /// <summary>
        ///     将字符串转换为long类型， 转换失败返回默认值
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        public static long ToLong(this string input, long defaultValue = 0) {
            if (long.TryParse(input, out var value)) {
                return value;
            }

            return defaultValue;
        }

        /// <summary>
        ///     To the decimal.
        /// </summary>
        /// <param name="val">The value.</param>
        public static decimal ToDecimal(this object val) {
            if (val.IsNullOrEmpty()) {
                return 0;
            }

            return Convert.ToDecimal(val.ToString());
        }

        /// <summary>
        ///     To the int16.
        /// </summary>
        /// <param name="val">The value.</param>
        public static short ToInt16(this object val) {
            if (val.IsNullOrEmpty()) {
                return 0;
            }

            return Convert.ToInt16(val.ToString());
        }

        /// <summary>
        ///     To the int64.
        /// </summary>
        /// <param name="val">The value.</param>
        public static long ToInt64(this object val) {
            if (val.IsNullOrEmpty()) {
                return 0;
            }

            if (long.TryParse(val.ToString(), out var result)) {
                return result;
            }

            return 0;
        }

        /// <summary>
        ///     转换Guid
        /// </summary>
        /// <param name="obj">The object.</param>
        public static Guid ToGuid(this object obj) {
            if (!obj.IsNullOrEmpty()) {
                if (!obj.IsGuidNullOrEmpty()) {
                    return Guid.Parse(obj.ToString());
                }
            }

            return Guid.NewGuid();
        }

        /// <summary>
        ///     To the boolean.
        /// </summary>
        /// <param name="val">The value.</param>
        public static bool ToBoolean(this object val) {
            if (!val.IsNullOrEmpty()) {
                if (val.ToString() == "1" || val.ToString().ToLower() == "true") {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     转换成ObjectId
        /// </summary>
        /// <param name="input"></param>
        public static ObjectId ToObjectId(this object input) {
            if (!input.IsNullOrEmpty()) {
                try {
                    ObjectId.Parse(input.ToStr());
                } catch {
                    return ObjectId.Empty;
                }
            }

            return ObjectId.Empty;
        }

        /// <summary>
        ///     To the date time.
        /// </summary>
        /// <param name="val">The value.</param>
        public static DateTime ToDateTime(this object val) {
            if (val.IsNullOrEmpty()) {
                return DateTime.Now;
            }

            var valid = DateTime.TryParse(val.ToString(), out var result);
            return result;
        }

        /// <summary>
        ///     Determines whether the specified input is email.
        /// </summary>
        /// <param name="input">The input.</param>
        public static bool IsEmail(this string input) {
            var match = Regex.Match(input,
                @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        ///     Determines whether the specified input is number.
        /// </summary>
        /// <param name="input">The input.</param>
        public static bool IsNumber(this string input) {
            var match = Regex.Match(input, @"^[0-9]+$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        #region 判断是否完整链接

        /// <summary>
        ///     判断是否完整链接
        /// </summary>
        /// <param name="url">The URL.</param>
        public static bool IsUrl(this string url) {
            url = url.Trim();
            if (url.StartsWith("http://")) {
                return true;
            }

            if (url.StartsWith("ftp://")) {
                return true;
            }

            if (url.StartsWith("https://")) {
                return true;
            }

            return false;
        }

        #endregion 判断是否完整链接

        /// <summary>
        ///     分割字符串
        /// </summary>
        /// <param name="source">来源字符串</param>
        /// <param name="spliter">分割符</param>
        /// <param name="limit">最大分割限制（小于等于0时为不限制）</param>
        public static IEnumerable<string> SplitString(this string source, string spliter, int limit = -1) {
            if (source == null) {
                yield break;
            }
            //如分割符为空则直接返回source
            if (string.IsNullOrEmpty(spliter)) {
                yield return source;
                yield break;
            }

            //循环分割字符串
            var count = 0;
            var index = 0;
            while (index >= 0) {
                var nextIndex = source.IndexOf(spliter, index);
                //如找不到下一个或已达到限制，返回剩余字符串
                if (nextIndex < 0 || limit > 0 && count >= limit - 1) {
                    yield return source.Substring(index);
                    yield break;
                }

                var part = source.Substring(index, nextIndex - index);
                index = nextIndex + spliter.Length;
                count++;
                yield return part;
            }
        }

        /// <summary>
        ///     根据空白字符分割字符串，支持跳过多个空白和过滤空字符串
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="count">最多分割的数量，-1为不限制</param>
        public static List<string> SplitBlank(this string text, int count = -1) {
            var strList = new List<string>();
            int offset = 0, x = 0;
            string str;
            while (offset < text.Length) {
                //跳过字符串前的空白
                if (BlankChars.Contains(text[offset])) {
                    offset++;
                    continue;
                }

                //如果没有达到分割限制
                if (count < 1 || strList.Count < count - 1) {
                    x = text.IndexOfAny(BlankChars, offset);
                    if (x < 0) //如果没有找到下一个空格，则截取到字符串末尾
{
                        x = text.Length;
                    }

                    str = text.Substring(offset, x - offset).Trim();
                    offset += str.Length;
                } else //如果已达到分割限制，读取剩余的所有字符
                  {
                    str = text.Substring(offset).Trim();
                    offset += str.Length;
                }

                //如果字符串不为空白则加入到返回列表中
                if (str.Length != 0) {
                    strList.Add(str);
                }
            }

            //返回分割后的字符串列表
            return strList;
        }

        /// <summary>
        ///     根据换行转换成数组
        /// </summary>
        /// <param name="str">The string.</param>
        public static List<string> SplitLine(this string str) {
            var lines = Regex.Split(str, "\r\n");
            var strList = new List<string>();
            foreach (var item in lines) {
                var text = item.Replace("/t", string.Empty);
                if (!text.Trim().IsNullOrEmpty()) {
                    strList.Add(text.Trim());
                }
            }

            return strList;
        }

        /// <summary>
        ///     替换所有字符串中的空白
        /// </summary>
        /// <param name="source">The source.</param>
        public static string ReplaceSpace(this string source) {
            foreach (var space in SpaceList) {
                source = source.Replace(space, string.Empty);
            }

            return source;
        }

        /// <summary>
        ///     字符串是否模糊包含（忽略空白）
        /// </summary>
        /// <param name="source">来源字符串</param>
        /// <param name="target">目标字符串</param>
        public static bool FuzzyContains(this string source, string target) {
            source = source.ReplaceSpace();
            target = target.ReplaceSpace();
            return !target.IsNullOrEmpty() && source.Contains(target);
        }

        /// <summary>
        ///     字符串是否模糊相等（忽略空白）
        /// </summary>
        /// <param name="source">来源字符串</param>
        /// <param name="target">目标字符串</param>
        public static bool FuzzyEquals(this string source, string target) {
            return source.ReplaceSpace() == target.ReplaceSpace();
        }

        /// <summary>
        ///     byte[]转换到base64字符串
        /// </summary>
        /// <param name="self">The self.</param>
        public static string EncodeToBase64(this byte[] self) {
            if (self.IsNullOrEmpty()) {
                return string.Empty;
            }

            return Convert.ToBase64String(self);
        }

        /// <summary>
        ///     base64字符串转换到byte，失败时返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="defaultValue"></param>
        public static byte[] DecodeAsBase64(this string self, byte[] defaultValue = null) {
            if (self.IsNullOrEmpty()) {
                return new byte[0];
            }

            try {
                return Convert.FromBase64String(self);
            } catch {
                return defaultValue;
            }
        }

        /// <summary>
        ///     截取字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="strLength">Length of the string.</param>
        public static string StrSub(this string str, int strLength = 10) {
            if (str.Length <= strLength) {
                return str;
            }

            return str.Substring(0, strLength);
        }

        /// <summary>
        ///     把byte[]转换到hex字符串
        /// </summary>
        /// <param name="self">The self.</param>
        public static string EncodeToHexdigest(this byte[] self) {
            if (self == null) {
                return string.Empty;
            }

            return BitConverter.ToString(self).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        ///     把hex字符串转换到byte[] 失败时返回default_value
        ///     参考：
        ///     http://stackoverflow.com/questions/321370/convert-hex-string-to-byte-array
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="defaultValue"></param>
        public static byte[] DecodeAsHexdigest(this string self, byte[] defaultValue = null) {
            if (self == null || self.Length % 2 != 0) {
                return defaultValue;
            }

            var bytes = new byte[self.Length / 2];
            for (var index = 0; index < bytes.Length; index++) {
                if (!byte.TryParse(self.Substring(index * 2, 2),
                    NumberStyles.HexNumber, CultureInfo.InvariantCulture, out bytes[index])) {
                    return defaultValue;
                }
            }

            return bytes;
        }

        /// <summary>
        ///     To the bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        public static byte[] ToBytes(this string str) {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        ///     移除换行
        /// </summary>
        /// <param name="str">The string.</param>
        public static string ToReplace(this string str) {
            return str.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
        }

        /// <summary>
        ///     将已经为在 URL 中传输而编码的字符串转换为解码的字符串
        /// </summary>
        /// <param name="str">The string.</param>
        public static string ToUrlDecode(this string str) {
            return WebUtility.UrlDecode(str);
        }

        /// <summary>
        ///     将文本字符串转换为 URL 编码的字符串。
        /// </summary>
        /// <param name="str">The string.</param>
        public static string ToUrlEncode(this string str) {
            return WebUtility.UrlEncode(str);
        }

        /// <summary>
        ///     计算字符在字符串中出现的次数
        /// </summary>
        /// <param name="self">来源字符串</param>
        /// <param name="target">要计算的字符</param>
        public static int CountChar(this string self, char target) {
            var count = 0;
            for (var n = 0; n < self.Length; n++) {
                if (self[n] == target) {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        ///     格式化字符串
        ///     和string.Format相同
        ///     http://stackoverflow.com/questions/16612080/how-would-i-write-a-string-format-extension-method
        /// </summary>
        /// <param name="self">来源字符串</param>
        /// <param name="args">格式化参数</param>
        public static string FormatWith(this string self, params object[] args) {
            if (self == null) {
                return null;
            }

            return string.Format(self, args);
        }

        /// <summary>
        ///     设置数组在指定位置的位的值
        ///     数组长度比需要的字节长度小时重新分配内存并调用realloced事件
        /// </summary>
        /// <param name="bytes">操作的数组</param>
        /// <param name="bitIndex"></param>
        /// <param name="value">设置的值</param>
        /// <param name="realloced">重新分配内存时调用的事件 没有重新分配时不会调用</param>
        public static void SetBitValue(
            this byte[] bytes, int bitIndex, bool value, Action<byte[]> realloced) {
            if (bitIndex < 0) {
                throw new ArgumentException("bit index must not be less than 0");
            }
            //计算长度
            var bitLength = bitIndex + 1; //位长度
            var byteLength = (bitLength + (8 - bitLength % 8) % 8) / 8; //字节长度
            //数组长度比需要的字节长度小时重新分配内存
            if (bytes == null || bytes.Length < byteLength) {
                bytes = bytes ?? new byte[0];
                Array.Resize(ref bytes, byteLength);
                realloced(bytes);
            }

            //设置指定位置的位的值
            if (value) {
                bytes[byteLength - 1] |= TrueByteMap[bitIndex % 8];
            } else {
                bytes[byteLength - 1] &= FalseByteMap[bitIndex % 8];
            }
        }

        /// <summary>
        ///     获取数组在在指定位置的位的值
        ///     数组长度比需要的字节长度小时返回null
        /// </summary>
        /// <param name="bytes">获取的数组</param>
        /// <param name="bitIndex"></param>
        public static bool? GetBitValue(this byte[] bytes, int bitIndex) {
            if (bitIndex < 0) {
                throw new ArgumentException("bit index must not be less than 0");
            }
            //计算长度
            var bitLength = bitIndex + 1; //位长度
            var byteLength = (bitLength + (8 - bitLength % 8) % 8) / 8; //字节长度
            //数组长度比需要的字节长度小时返回null
            if (bytes == null || bytes.Length < byteLength) {
                return null;
            }
            //获取指定位置的位的值
            var value = bytes[byteLength - 1] & TrueByteMap[bitIndex % 8];
            return value > 0;
        }

        /// <summary>
        ///     首字母大写
        ///     如果isLower为true则，后面的字母全部小写。
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="isLower">后面的字母是否全部换成小写</param>
        public static string ToFristUpper(this string str, bool isLower = false) {
            if (!str.IsNullOrEmpty()) {
                if (str.Length == 1) {
                    return str = str.Substring(0, 1).ToUpper();
                }

                if (str.Length > 1) {
                    if (!isLower) {
                        return str = str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
                    }

                    return str = str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1).ToLower();
                }
            }

            return string.Empty;
        }

        public static string ToFristLower(this string str) {
            if (!str.IsNullOrEmpty()) {
                if (str.Length == 1) {
                    return str = str.Substring(0, 1).ToLower();
                }

                if (str.Length > 1) {
                    return str = str.Substring(0, 1).ToLower() + str.Substring(1, str.Length - 1);
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///     To the string.
        /// </summary>
        /// <param name="input">The input.</param>
        public static string ToStr(this object input) {
            if (input.IsNullOrEmpty()) {
                return string.Empty;
            }

            return input.ToString();
        }

        /// <summary>
        ///     To the encoding.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="charset">The charset.</param>
        public static string ToEncoding(this string str, string charset = "utf-8") {
            return Encoding.GetEncoding(charset).GetString(ToBytes(str));
        }

        /// <summary>
        ///     To the camel case string.
        /// </summary>
        /// <param name="s">The s.</param>
        public static string ToCamelCaseString(this string s) {
            if (string.IsNullOrWhiteSpace(s)) {
                return string.Empty;
            }

            if (char.IsUpper(s[0])) {
                return $"{char.ToLower(s[0])}{s.Substring(1)}";
            }

            return s;
        }

        /// <summary>
        ///     Replaces the HTML tag.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="length">The length.</param>
        public static string ReplaceHtmlTag(this string html, int length = 0) {
            if (!html.IsNullOrEmpty()) {
                var strText = Regex.Replace(html, "<[^>]+>", string.Empty);
                strText = Regex.Replace(strText, "&[^;]+;", string.Empty);

                if (length > 0 && strText.Length > length) {
                    return strText.Substring(0, length);
                }

                return strText;
            }

            return string.Empty;
        }

        #region 过滤字符串中的sql命令

        /// <summary>
        ///     过滤字符串中的sql命令
        /// </summary>
        /// <param name="sInput">The s input.</param>
        public static string Filter(this string sInput) {
            if (sInput == null || sInput == string.Empty) {
                return null;
            }

            var sInput1 = sInput.ToLower();
            var output = sInput;
            var pattern = "*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.IgnoreCase).Success) {
                output = output.Replace(sInput, "''");
            } else {
                output = output.Replace("'", "''");
            }

            return output;
        }

        #endregion 过滤字符串中的sql命令

        #region 解码网页参数中的字符串

        /// <summary>
        ///     解码网页参数中的字符串
        /// </summary>
        /// <param name="text">The text.</param>
        public static string InputTexts(this string text) {
            if (string.IsNullOrEmpty(text)) {
                return string.Empty;
            }

            text = Regex.Replace(text, @"[\s]{2,}", " ");
            text = Regex.Replace(text, @"(<[b|B][r|R]/*>)+|(<[p|P](.|\n)*?>)", "\n");
            text = Regex.Replace(text, @"(\s*&[n|N][b|B][s|S][p|P];\s*)+", " ");
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            text = text.Replace("'", "''");
            return text;
        }

        #endregion 解码网页参数中的字符串

        /// <summary>
        ///     Url Send HttpPost
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramData">PostData</param>
        /// <param name="contentType">default = "application/json"</param>
        /// <returns></returns>
        public static string Post(this string url, string paramData, Dictionary<string, string> dicHeader = null) {
            return Post(url, paramData, Encoding.UTF8, dicHeader);
        }

        private static string Post(string url, string paramData, Encoding encoding,
            Dictionary<string, string> dicHeader = null) {
            string result;

            if (url.ToLower().IndexOf("https", StringComparison.Ordinal) > -1) {
                ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, errors) => { return true; };
            }

            var wc = new WebClient();
            if (string.IsNullOrEmpty(wc.Headers["Content-Type"])) {
                var contentType = "application/json";
                wc.Headers.Add("Content-Type", contentType);
            }

            if (dicHeader != null) {
                foreach (var item in dicHeader) {
                    wc.Headers.Add(item.Key, item.Value);
                }
            }

            wc.Encoding = encoding;

            result = wc.UploadString(url, "POST", paramData);

            return result;
        }

        /// <summary>
        ///     Url Sent HttpGet
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(this string url) {
            return HttpGet(url, Encoding.UTF8);
        }

        private static string HttpGet(string url, Encoding encoding) {
            try {
                var wc = new WebClient { Encoding = encoding };
                var readStream = wc.OpenRead(url);
                using (var sr = new StreamReader(readStream, encoding)) {
                    var result = sr.ReadToEnd();
                    return result;
                }
            } catch (Exception e) {
                return e.Message;
            }
        }

        #region 截取字符串

        /// <summary>
        ///     截取字符串
        /// </summary>
        /// <param name="soucreStr">The soucre string.</param>
        /// <param name="len">长度</param>
        public static string Substring(this string soucreStr, int len) {
            var result = soucreStr;
            if (soucreStr.Length >= len) {
                result = soucreStr.Substring(0, len);
            }

            return result;
        }

        /// <summary>
        ///     根据开始字符串和结束字符串来截取子字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="beginStr">开始字符串，必须唯一</param>
        /// <param name="endStr">结束字符串，必须唯一</param>
        public static string CutString(this string str, string beginStr, string endStr) {
            var begin = str.IndexOf(beginStr) + beginStr.Length;
            var end = str.LastIndexOf(endStr);
            var lenth = end - begin;
            if (lenth > 0) {
                return str.Substring(begin, lenth);
            }

            return string.Empty;
        }

        /// <summary>
        ///     根据开始字符串和结束字符串来截取子字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="endStr">开始字符串，必须唯一</param>
        public static string SubStringEnd(this string str, string endStr) {
            var begin = str.LastIndexOf(endStr, StringComparison.Ordinal) + endStr.Length;
            var end = str.Length;
            var lenth = end - begin;
            if (lenth > 0) {
                return str.Substring(begin, lenth);
            }

            return string.Empty;
        }

        /// <summary>
        ///     根据开始字符串和结束字符串来截取子字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="beginStr">开始字符串，必须唯一</param>
        public static string SubStringEndLast(this string str, string beginStr) {
            var begin = str.LastIndexOf(beginStr, StringComparison.Ordinal) + beginStr.Length;
            var end = str.Length;
            var lenth = end - begin;
            if (lenth > 0) {
                return str.Substring(begin, lenth);
            }

            return string.Empty;
        }

        /// <summary>
        ///     根据开始字符串和结束字符串来截取子字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="beginStr">开始字符串，必须唯一</param>
        public static string SubStringLastEnd(this string str, string beginStr) {
            var begin = str.LastIndexOf(beginStr, StringComparison.Ordinal);
            if (begin > 0) {
                return str.Substring(0, begin);
            }

            return string.Empty;
        }

        #endregion 截取字符串
    }

    /// <summary>
    ///     ContentTypes List for reference using
    /// </summary>
    public class ContentTypes {

        /// <summary>
        ///     application/json
        /// </summary>
        public static string ApplicationJson = "application/json";

        /// <summary>
        ///     application/x-www-form-urlencoded
        /// </summary>
        public static string ApplicationFromUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        ///     text/plain
        /// </summary>
        public static string TextPlain = "text/plain";

        /// <summary>
        ///     application/javascript
        /// </summary>
        public static string ApplicationJavascript = "application/javascript";

        /// <summary>
        ///     application/xml
        /// </summary>
        public static string ApplicationXml = "application/xml";

        /// <summary>
        ///     text/xml
        /// </summary>
        public static string TextXml = "text/xml";

        /// <summary>
        ///     text/html
        /// </summary>
        public static string TextHtml = "text/html";
    }
}