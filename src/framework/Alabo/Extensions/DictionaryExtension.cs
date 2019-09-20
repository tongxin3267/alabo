using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Alabo.Extensions
{
    /// <summary>
    ///     Class DictionaryExtension.
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        ///     删除值，不区分大小写
        ///     Removes the key.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        public static Dictionary<T, TV> RemoveKey<T, TV>(this Dictionary<T, TV> self, string key)
        {
            var dic = new Dictionary<T, TV>();
            self.Foreach(r =>
            {
                var dicKey = r.Key.ToStr();
                if (!dicKey.Equals(key, StringComparison.OrdinalIgnoreCase)) {
                    dic.Add(r.Key, r.Value);
                }
            });
            return dic;
        }

        /// <summary>
        ///     Removes the key.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="keys">The keys.</param>
        public static Dictionary<T, TV> RemoveKey<T, TV>(this Dictionary<T, TV> self, params string[] keys)
        {
            foreach (var key in keys) {
                self = self.RemoveKey(key);
            }

            return self;
        }

        /// <summary>
        ///     删除字典中空的值
        /// </summary>
        /// <param name="self">The self.</param>
        public static Dictionary<T, TV> RemoveNullOrEmpty<T, TV>(this Dictionary<T, TV> self)
        {
            var dic = new Dictionary<T, TV>();
            self.Foreach(r =>
            {
                if (!r.Value.ToStr().IsNullOrEmpty()) {
                    dic.Add(r.Key, r.Value);
                }
            });
            return dic;
        }

        /// <summary>
        ///     尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key,
            TValue value)
        {
            if (dict.ContainsKey(key) == false) {
                dict.Add(key, value);
            }

            return dict;
        }

        /// <summary>
        ///     将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key,
            TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        ///     向字典中批量添加键值对
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="values">The values.</param>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values) {
                if (dict.ContainsKey(item.Key) == false || replaceExisted) {
                    dict[item.Key] = item.Value;
                }
            }

            return dict;
        }

        /// <summary>
        ///     从词典中获取值，如果没有则返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static TV GetValue<T, TV>(this IDictionary<T, TV> self, T key, TV defaultValue = default)
        {
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        ///     从词典中获取字符串，如果没有则返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static string GetString<T, TV>(this IDictionary<T, TV> self, T key, string defaultValue = null)
        {
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            return value.ConvertToString(defaultValue);
        }

        /// <summary>
        ///     从词典中获取整数，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static int GetInt<T, TV>(this IDictionary<T, TV> self, T key, int defaultValue = -1)
        {
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            if (!int.TryParse(value.ConvertToString(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     从词典中获取布尔值，必须指定默认值
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static bool GetBool<T, TV>(this IDictionary<T, TV> self, T key, bool defaultValue)
        {
            if (key == null) {
                return defaultValue;
            }

            var value = self.GetNullableBool(key);
            if (value.HasValue) {
                return value.Value;
            }

            return defaultValue;
        }

        /// <summary>
        ///     从词典中获取nullable布尔值，获取失败时设置为默认值
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static bool? GetNullableBool<T, TV>(this IDictionary<T, TV> self, T key, bool? defaultValue = null)
        {
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            return value.ConvertToNullableBool();
        }

        /// <summary>
        ///     从词典中获取小数，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static decimal GetDecimal<T, TV>(this IDictionary<T, TV> self, T key, decimal defaultValue = -1)
        {
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            if (!decimal.TryParse(value.ConvertToString(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     从词典中获取时间，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static DateTime GetDateTime<T, TV>(this IDictionary<T, TV> self, T key,
            DateTime defaultValue = default)
        {
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            if (!DateTime.TryParse(value.ConvertToString(), out var result)) {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        ///     从词典中获取数字列表，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        public static IList<int> GetIntList<T, TV>(this IDictionary<T, TV> self, T key, IList<int> defaultValue = null)
        {
            IList<int> result;
            if (key == null) {
                return defaultValue;
            }

            if (!self.TryGetValue(key, out var value)) {
                return defaultValue;
            }

            if (value == null) {
                return null;
            }

            result = value.ConvertToString().ConvertToIntList();
            return result;
        }

        /// <summary>
        ///     从词典中获取值，不存在时使用new新建
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        public static TV GetCreateValue<T, TV>(this IDictionary<T, TV> self, T key) where TV : new()
        {
            if (!self.TryGetValue(key, out var value))
            {
                value = new TV();
                self[key] = value;
                return value;
            }

            return value;
        }

        /// <summary>
        ///     从指定词典或列表合并，可以选择是否覆盖原来的值
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="source">The source.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        public static void Merge<T, TV>(this IDictionary<T, TV> self, IEnumerable<KeyValuePair<T, TV>> source,
            bool replace = true)
        {
            foreach (var pair in source)
            {
                if (!replace && self.ContainsKey(pair.Key)) {
                    continue;
                }

                self[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        ///     获取s the vaule.
        /// </summary>
        /// <param name="dic">The dic.</param>
        /// <param name="key">The key.</param>
        public static string GetVaule(this IDictionary<long, string> dic, long key)
        {
            if (dic.TryGetValue(key, out var str)) {
                return str;
            }

            return string.Empty;
        }

        /// <summary>
        ///     获取s the vaule.
        /// </summary>
        /// <param name="dic">The dic.</param>
        /// <param name="key">The key.</param>
        public static string GetVaule(this IDictionary<string, string> dic, string key)
        {
            if (dic.TryGetValue(key, out var str)) {
                return str;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Url中的值，转成字典类型
        /// </summary>
        /// <param name="httpContext"></param>
        public static Dictionary<string, string> ToDictionary(this HttpContext httpContext)
        {
            var dictionary = new Dictionary<string, string>();

            // 指定搜索
            var currentMode = httpContext.Request.Query["_currentMode"];

            var keyDictionary = httpContext.Request.Query.Keys;

            foreach (var item in keyDictionary)
            {
                // 指定搜索
                if (!currentMode.ToString().IsNullOrEmpty()) {
                    if (!(item == currentMode || item == "Service" || item == "Method"))
                    {
                        continue;
                        ;
                    }
                }

                var value = httpContext.Request.Query[item];
                if (value == "-1" || item == "_currentMode") {
                    continue;
                }

                var list = value.ToString().SplitList(new[] {','});
                var defaultValue = list.FirstOrDefault();
                if (!defaultValue.IsNullOrEmpty()) {
                    dictionary.Add(item, defaultValue);
                }
            }

            return dictionary;
        }

        /// <summary>
        ///     Url中的值，转成NameValueCollection
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public static NameValueCollection ToNameValueCollection(this HttpContext httpContext)
        {
            var nameValueCollection = new NameValueCollection();
            var keyDictionary = httpContext.Request.Query.Keys;
            foreach (var item in keyDictionary)
            {
                var value = httpContext.Request.Query[item];
                if (value == "-1" || item == "_currentMode") {
                    continue;
                }

                var list = value.ToString().SplitList(new[] {','});
                var defaultValue = list.FirstOrDefault();
                if (!defaultValue.IsNullOrEmpty()) {
                    nameValueCollection.Add(item, defaultValue);
                }
            }

            return nameValueCollection;
        }

        /// <summary>
        ///     字典转成Url
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="baseUrl"></param>
        public static string DictionaryToUrl(this Dictionary<string, string> dic, string baseUrl)
        {
            var sb = new StringBuilder();
            if (!baseUrl.Contains("Basic/List"))
            {
                dic = dic.RemoveKey("service");
                dic = dic.RemoveKey("method");
            }

            foreach (var item in dic)
            {
                var value = item.Value;
                if (item.Key != "PageIndex") {
                    sb.AppendFormat("{0}={1}&", item.Key, item.Value);
                }
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "?");
                sb.Remove(sb.Length - 1, 1);
            }

            var url = baseUrl + sb;
            return url;
        }
    }
}