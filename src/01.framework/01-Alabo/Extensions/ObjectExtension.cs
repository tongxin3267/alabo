using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Mapping;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Alabo.Extensions {

    public static class ObjectExtension {

        /// <summary>
        ///     将字符串生成固定的ObjectId
        /// </summary>
        /// <param name="value"></param>
        public static ObjectId ConvertToObjectId(this string value) {
            value = value.Trim().ToLower();

            var data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value));
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++) {
                sb.AppendFormat("{0:x2}", data[i]);
            }

            var md5Value = sb.ToString();
            md5Value = md5Value.Replace("-", "").ToLower().Substring(0, 24);

            var objectId = ObjectId.Parse(md5Value);
            return objectId;
        }

        public static T Cast<T>(this object obj, T t) {
            return (T)obj;
        }

        /// <summary>
        ///     将object类型转换为List类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="t"></param>
        public static List<T> ConvertToList<T>(this object obj, T t) {
            var list = new List<T>();
            return list ?? default;
        }

        /// <summary>
        ///     避免与mongdb tojson重复命名
        /// </summary>
        /// <param name="obj"></param>
        public static string ToJsons(this object obj) {
            return ToJson(obj);
        }

        /// <summary>
        ///     将object对象序列化成json字符串
        ///     转换成序列化对象
        ///     时间格式统一转化为 yyyy-MM-dd HH:mm:ss  不在使用 JsonConvert 默认的类型
        /// </summary>
        /// <typeparam name="obj"></typeparam>
        /// <param name="obj"></param>
        public static string ToJson(this object obj) {
            try {
                var timeFormat = new IsoDateTimeConverter {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                if (obj == null) {
                    obj = string.Empty;
                }

                return JsonConvert.SerializeObject(obj, Formatting.Indented, timeFormat);
            } catch {
                return JsonConvert.SerializeObject(obj);
            }
        }

        /// <summary>
        ///     驼峰命名
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsoCamelCase(this object obj) {
            try {
                var timeFormat = new IsoDateTimeConverter {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                if (obj == null) {
                    obj = string.Empty;
                }

                var serializerSettings = new JsonSerializerSettings {
                    // 设置为驼峰命名
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                return JsonConvert.SerializeObject(obj, Formatting.Indented, serializerSettings);
            } catch {
                return JsonConvert.SerializeObject(obj);
            }
        }

        /// <summary>
        ///     将object对象序列化成json字符串
        ///     转换成序列化对象
        ///     时间格式统一转化为 yyyy-MM-dd HH:mm:ss  不在使用 JsonConvert 默认的类型
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        public static string ToJson(this object obj, Type type) {
            try {
                var options = new JsonSerializerSettings {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var value = JsonConvert.SerializeObject(obj, type, options);
                return value;
            } catch {
                return JsonConvert.SerializeObject(obj);
            }
        }

        /// <summary>
        ///     将表单对象转成指定的Json数据，会过滤掉其他所有非表单数据
        /// </summary>
        /// <param name="httpContext">The form collection.</param>
        /// <param name="type">The 类型.</param>
        public static string ToJson(HttpContext httpContext, Type type) {
            var obj = AutoMapping.SetValue(httpContext, type);
            return obj.ToJson();
        }

        /// <summary>
        ///     To the json.
        ///     默认值json对象
        ///     将表单Type转成指定的Json数据，先实例化对象
        /// </summary>
        /// <param name="type">The 类型.</param>
        public static string ToJson(this Type type) {
            var instance = Activator.CreateInstance(type);
            return instance?.ToJson();
        }

        /// <summary>
        ///     反序列化，转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static T ToObject<T>(this object obj) where T : new() {
            return DeserializeJson<T>(obj);
        }

        /// <summary>
        ///     反序列化，转换成对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fullName"></param>
        public static object ToObject(this object obj, string fullName) {
            var type = fullName.GetTypeByFullName();
            return ToObject(obj, type);
        }

        /// <summary>
        ///     反序列化，转换成对象
        /// </summary>
        /// <param name="obj"></param>
        /// <paramref name="type" />
        public static object ToObject(this object obj, Type type) {
            if (obj.IsNullOrEmpty()) {
                return null;
            }

            if (type == null) {
                return null;
            }

            var result = JsonConvert.DeserializeObject(obj.ToStr(), type);
            return result;
        }

        /// <summary>
        ///     反序列化，转换成对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToObjectIgnoreNull(this object obj, Type type) {
            if (obj.IsNullOrEmpty()) {
                return null;
            }

            if (type == null) {
                return null;
            }

            var jsonSerializerSettings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore
            };

            var result = JsonConvert.DeserializeObject(obj.ToStr(), type, jsonSerializerSettings);
            return result;
        }

        /// <summary>
        ///     反序列化json对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static T DeserializeJson<T>(this object obj) where T : new() {
            if (obj == null) {
                obj = string.Empty;
            }

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            settings.Converters.Add(new ObjectIdConverter());

            var result = JsonConvert.DeserializeObject<T>(obj.ToString());
            if (result == null) {
                result = new T();
            }

            return result;
        }

        public static object DeserializeJson<T>(this string jsonString, T defaultValue) {
            if (jsonString == null) {
                throw new ArgumentNullException(nameof(jsonString));
            }

            var result = JsonConvert.DeserializeAnonymousType(jsonString, defaultValue);
            return result;
        }

        public static T ToClass<T>(this object obj) {
            if (obj != null) {
                return (T)obj;
            }

            return JsonConvert.DeserializeObject<T>(string.Empty);
        }

        /// <summary>
        ///     把object转换为string，失败时返回default_value
        /// </summary>
        public static string ConvertToString(this object obj, string defaultValue = "") {
            if (obj == null) {
                return defaultValue;
            }

            return obj.ToString() ?? defaultValue;
        }

        /// <summary>
        ///     把object转换为int，失败时返回default_value
        /// </summary>
        public static int ConvertToInt(this object obj, int defaultValue = -1) {
            if (obj == null) {
                return defaultValue;
            }

            if (obj is int) {
                return (int)obj;
            }

            if (!int.TryParse(obj.ToString().Trim(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     把object转换为long，失败时返回default_value
        /// </summary>
        public static long ConvertToLong(this object obj, long defaultValue = -1) {
            if (obj == null) {
                return defaultValue;
            }

            if (obj is long) {
                return (long)obj;
            }

            if (!long.TryParse(obj.ToString().Trim(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     把object转换为decimal，失败时返回default_value
        /// </summary>
        public static decimal ConvertToDecimal(this object obj, decimal defaultValue = -1M) {
            if (obj == null) {
                return defaultValue;
            }

            if (obj is decimal) {
                return (decimal)obj;
            }

            if (!decimal.TryParse(obj.ToString().Trim(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     把object转换为double，失败时返回default_value
        /// </summary>
        public static double ConvertToDouble(this object obj, double defaultValue = -1) {
            if (obj == null) {
                return defaultValue;
            }

            if (obj is double) {
                return (double)obj;
            }

            if (!double.TryParse(obj.ToString().Trim(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     把object转换为DateTime，失败时返回default_value
        /// </summary>
        public static DateTime ConvertToDateTime(this object obj, DateTime defaultValue) {
            if (obj == null) {
                return defaultValue;
            }

            if (obj is DateTime) {
                return (DateTime)obj;
            }

            if (!DateTime.TryParse(obj.ToString().Trim(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     把object转换为DateTime，失败时返回1900年1月1日
        /// </summary>
        public static DateTime ConvertToDateTime(this object obj) {
            return obj.ConvertToDateTime(new DateTime(1900, 1, 1));
        }

        /// <summary>
        ///     Javascript TimeStamp long convert to datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeOfTimeStamp(this object obj) {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            var value = obj.ConvertToLong();
            if (value > 0) {
                return startTime.AddMilliseconds(value);
            }

            return new DateTime(1900, 1, 1);
        }

        /// <summary>
        ///     把object转换为utc dateTime，失败时返回default_value
        ///     默认判断传入的来源时间是本地时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        public static DateTime ConvertToUtcDateTime(this object obj, DateTime defaultValue) {
            return obj.ConvertToUtcDateTime() ?? defaultValue;
        }

        /// <summary>
        ///     把object转换为utc dateTime?，失败时返回null
        ///     默认判断传入的来源时间是本地时间
        /// </summary>
        /// <param name="obj"></param>
        public static DateTime? ConvertToUtcDateTime(this object obj) {
            return obj.ConvertToUtcDateTime(DateTimeKind.Local);
        }

        /// <summary>
        ///     把object转换为utc dateTime?，失败时返回null
        ///     传入的来源时间的类型是Unspecified时 指定为传入的source_kind
        ///     再转换到utc时间返回
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sourceKind"></param>
        public static DateTime? ConvertToUtcDateTime(this object obj, DateTimeKind sourceKind) {
            DateTime result;
            if (obj == null) {
                return null;
            }

            if (obj is DateTime) {
                result = (DateTime)obj;
            } else if (DateTime.TryParse(obj.ConvertToString(string.Empty).Trim(), out result)) {
                GC.KeepAlive(result);
            } else {
                return null;
            }
            //TODO: 进行时区转换
            if (result.Kind == DateTimeKind.Unspecified) {
                result = DateTime.SpecifyKind(result, sourceKind);
            }

            result = result.ToUniversalTime();
            return result;
        }

        /// <summary>
        ///     把object转换为Guid，失败时返回Guid.Empty
        /// </summary>
        public static Guid ConvertToGuid(this object obj) {
            if (obj == null) {
                return Guid.Empty;
            }

            if (obj is Guid) {
                return (Guid)obj;
            }

            if (!Guid.TryParse(obj.ToString().Trim(), out var guid)) {
                return Guid.Empty;
            }

            return guid;
        }

        /// <summary>
        ///     把object转换为bool，失败时返回default_value
        /// </summary>
        public static bool ConvertToBool(this object obj, bool defaultValue = false) {
            if (obj.IsNullOrEmpty()) {
                return false;
            }

            var value = obj.ConvertToNullableBool();
            if (value.HasValue) {
                return value.Value;
            }

            return defaultValue;
        }

        /// <summary>
        ///     把object转换为bool?，失败时返回null
        /// </summary>
        public static bool? ConvertToNullableBool(this object obj) {
            if (obj == null) {
                return null;
            }

            if (obj is bool) {
                return (bool)obj;
            }

            var value = obj.ToString().ToLower();
            if (value == "true") {
                return true;
            }

            if (value == "false") {
                return false;
            }

            if (!int.TryParse(value, out var x)) {
                return null;
            }

            if (x != 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     把object换为int列表（如object是字符串，则按逗号分割并转换）
        /// </summary>
        public static List<int> ConvertToIntList(this object data) {
            //如果字符串为null则返回空列表
            var result = new List<int>();
            if (data == null) {
                return result;
            }

            if (data is List<int>) {
                return (List<int>)data;
            }

            if (data is IEnumerable<int>) {
                return ((IEnumerable<int>)data).ToList();
            }

            var str = data.ToString().Trim();
            //按逗号分割并添加到列表
            foreach (var s in str.Split(',')) {
                if (int.TryParse(s.Trim(), out var n)) {
                    result.Add(n);
                }
            }

            return result;
        }

        /// <summary>
        ///     把object换为uint列表（如object是字符串，则按逗号分割并转换）
        /// </summary>
        public static List<uint> ConvertToUIntList(this object data) {
            //如果字符串为null则返回空列表
            var result = new List<uint>();
            if (data == null) {
                return result;
            }

            if (data is List<uint>) {
                return (List<uint>)data;
            }

            if (data is IEnumerable<uint>) {
                return ((IEnumerable<uint>)data).ToList();
            }

            var str = data.ToString().Trim();
            //按逗号分割并添加到列表
            foreach (var s in str.Split(',')) {
                if (uint.TryParse(s.Trim(), out var n)) {
                    result.Add(n);
                }
            }

            return result;
        }

        /// <summary>
        ///     把object转换为Uri，失败或不匹配时返回null
        /// </summary>
        /// <param name="data">要转换的对象</param>
        /// <param name="kind">url的类型</param>
        /// <param name="allowedScheme"></param>
        public static Uri ConvertToUri(this object data, UriKind kind, params string[] allowedScheme) {
            if (data == null) {
                return null;
            }
            //直接转换
            var uri = data as Uri;
            //直接转换失败时解析 解析失败时返回null
            if (uri == null && !Uri.TryCreate(data.ToString(), kind, out uri)) {
                return null;
            }
            //判断是否匹配
            if (allowedScheme.IsNullOrEmpty()) {
                allowedScheme = new[] { "http", "https" };
            }

            if (kind == UriKind.Relative && uri.IsAbsoluteUri ||
                kind == UriKind.Absolute && !uri.IsAbsoluteUri ||
                uri.IsAbsoluteUri && !allowedScheme.Contains(uri.Scheme)) {
                return null;
            }

            return uri;
        }

        /// <summary>
        ///     使用指定类型复制对象，可用于强制转换到继承的类，不会递归复制
        /// </summary>
        /// <typeparam name="T">要复制到的类型</typeparam>
        /// <param name="self">要复制的对象</param>
        /// <returns>复制后的对象</returns>
        public static T CopyObject<T>(this T self) where T : class {
            if (self == null) {
                return null;
            }

            var type = typeof(T);
            //创建目标类型的对象
            var result = (T)Activator.CreateInstance(typeof(T));
            //复制所有字段
            var fieldList = type.GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fieldList) {
                field.SetValue(result, field.GetValue(self));
            }

            return result;
        }

        /// <summary>
        ///     同名属性值复制
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="source">被复制的对象</param>
        public static void CopyPropertyValuesFrom(this object obj, object source) {
            if (obj is IEnumerable) {
                var destEnumerator = (obj as IEnumerable).GetEnumerator();
                var srcEnumerator = (obj as IEnumerable).GetEnumerator();
                while (destEnumerator.MoveNext() && srcEnumerator.MoveNext()) {
                    destEnumerator.Current.CopyPropertyValuesFrom(srcEnumerator.Current);
                }
            } else {
                foreach (var propertyInfo in source.GetType().GetProperties()) {
                    var destPropertyInfo = obj.GetType().GetProperty(propertyInfo.Name);
                    if (destPropertyInfo == null) {
                        continue;
                    }

                    if (destPropertyInfo.CanWrite) {
                        destPropertyInfo.SetValue(obj, propertyInfo.GetValue(source, null), null);
                    }
                }
            }
        }

        /// <summary>
        ///     把object转换到Nullable&lt;DateTime&gt;
        /// </summary>
        /// <param name="obj"></param>
        public static DateTime? ConvertToNullableDateTime(this object obj) {
            if (obj == null) {
                return null;
            }

            if (obj is DateTime) {
                return (DateTime)obj;
            }

            if (!DateTime.TryParse(obj.ToString().Trim(), out var result)) {
                return null;
            }

            return result;
        }

        public static bool CheckJsonIsEmpty(this string str) {
            if (string.IsNullOrEmpty(str) || str == "[]" || str == "{}") {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     判断是否能反序列化
        /// </summary>
        /// <param name="str"></param>
        public static bool IsJsonDeserialize<T>(this string str) {
            try {
                JsonConvert.DeserializeObject<T>(str);
            } catch (Exception) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     标签截取
        /// </summary>
        /// <param name="charStrand">字符串</param>
        /// <param name="label">截取的标签 标签必须是 XXX="xxxx"</param>
        /// <returns>如果字符串不存在则放回空 存在则返回存在的值</returns>
        public static string Substring(this string charStrand, string label) {
            var index = charStrand.IndexOf(label + "=\"", StringComparison.OrdinalIgnoreCase);
            if (index < 1) {
                return charStrand;
            }

            charStrand = charStrand.Substring(index + (label + "=\"").Length);
            index = charStrand.IndexOf("\"", StringComparison.OrdinalIgnoreCase);
            return charStrand.Substring(0, index);
        }

        public static string Replaces(this string str, string strRep, string rep) {
            var index = str.IndexOf(strRep, StringComparison.OrdinalIgnoreCase);
            if (index < 1) {
                return str;
            }

            var strStart = str.Substring(0, index);
            var strStop = str.Substring(index + strRep.Length);
            return string.Format("{0}{1}{2}", strStart, rep, strStop);
        }

        public static int GetCount(this string str, string strCount) {
            var result = 0;
            if (string.IsNullOrEmpty(str)) {
                return result;
            }

            var index = str.IndexOf(strCount, StringComparison.OrdinalIgnoreCase);
            if (index < 1) {
                return result;
            }

            str = str.Substring(index + strCount.Length);
            while (index > 0) {
                index = str.IndexOf(strCount, StringComparison.OrdinalIgnoreCase);
                str = str.Substring(index + strCount.Length);
                result++;
            }

            return result;
        }

        /// <summary>
        ///     将一个节点插入另外一个之后
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newChild"></param>
        /// <param name="refChild"></param>
        public static string InsertAfter(this string str, string newChild, string refChild) {
            var index = str.IndexOf(refChild);
            if (index < 0) {
                return str;
            }

            var strart = str.Substring(0, index + refChild.Length);
            var stop = str.Substring(index + refChild.Length);
            return $"{strart}{newChild}{stop}";
        }

        /// <summary>
        ///     将一个节点插入另外一个前
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newChild"></param>
        /// <param name="refChild"></param>
        public static string InsertBefore(this string str, string newChild, string refChild) {
            var index = str.IndexOf(refChild);
            if (index < 0) {
                return str;
            }

            var strart = str.Substring(0, index);
            var stop = str.Substring(index);
            return $"{strart}{newChild}{stop}";
        }

        public static string RemoveIndexOf(this string str, string refChild) {
            var index = str.IndexOf(refChild);
            if (index < 0) {
                return str;
            }

            var strart = str.Substring(0, index);
            var stop = str.Substring(index + refChild.Length);
            return $"{strart}{stop}";
        }

        /// <summary>
        ///     在已有的css中添加样式
        /// </summary>
        /// <param name="cssContext">css本身内容</param>
        /// <param name="cssKey">要操作的css 不加 .</param>
        /// <param name="cssContexts">css的实际内容 如: width:100px;height:100px;</param>
        /// <returns>操作后的css本身</returns>
        public static string AddCss(this string cssContext, string cssKey, string cssContexts) {
            var sb = new StringBuilder();
            sb.Append(cssContext.RemoveCssContext(cssKey));
            sb.AppendFormat("\r\n.{0}", cssKey);
            sb.Append("{\r\n");
            sb.Append(cssContexts);
            sb.Append("\r\n}");

            return sb.ToString();
        }

        /// <summary>
        ///     需要移除的css
        /// </summary>
        /// <param name="cssContext">css内容</param>
        /// <param name="cssKey">需要移除的css值  不加. </param>
        public static string RemoveCssContext(this string cssContext, string cssKey) {
            var index = cssContext.IndexOf($".{cssKey}");
            index = index < 0 ? 0 : index;
            var stop = cssContext.IndexOf("}", index);
            stop = stop < 0 ? 0 : stop;
            return $"{cssContext.Substring(0, index)}{cssContext.Substring(stop)}";
        }

        public static string LastChar(this string str, string cha = ".") {
            return str.Substring(str.LastIndexOf(cha) + 1);
        }
    }
}