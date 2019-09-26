using System;
using System.Collections.Generic;

namespace Alabo.Helpers
{
    #region 词典读取器

    /// <summary>
    ///     词典读取器
    ///     可以自动获取和转换类似int, decimal等数值
    /// </summary>
    /// <typeparam name="T">键的类型</typeparam>
    /// <typeparam name="V">值的类型</typeparam>
    public class DictionaryHelper<T, TV>
    {
        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="dict">要读取的词典</param>
        public DictionaryHelper(IDictionary<T, TV> dict)
        {
            _dict = dict;
        }

        #endregion 初始化

        #region 从词典中获取值，如果没有则返回default_value

        /// <summary>
        ///     从词典中获取值，如果没有则返回default_value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public TV GetValue(T key, TV defaultValue = default)
        {
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            return value;
        }

        #endregion 从词典中获取值，如果没有则返回default_value

        #region 从词典中获取字符串，如果没有则返回default_value

        /// <summary>
        ///     从词典中获取字符串，如果没有则返回default_value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public string GetString(T key, string defaultValue = null)
        {
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            return value.ToString();
        }

        #endregion 从词典中获取字符串，如果没有则返回default_value

        #region 从词典中获取整数，如果没有或转换失败则返回default_value

        /// <summary>
        ///     从词典中获取整数，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public int GetInt(T key, int defaultValue = -1)
        {
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            if (!int.TryParse(value.ToString(), out var result)) return defaultValue;

            return result;
        }

        #endregion 从词典中获取整数，如果没有或转换失败则返回default_value

        #region 尝试转换布尔值，失败时返回null

        /// <summary>
        ///     尝试转换布尔值，失败时返回null
        /// </summary>
        /// <param name="value"></param>
        public bool? StringToBool(string value)
        {
            if (value == null) return null;

            value = value.ToLower();
            if (value == "true") return true;

            if (value == "false") return false;

            if (!int.TryParse(value, out var x)) return null;

            if (x != 0) return true;

            return false;
        }

        #endregion 尝试转换布尔值，失败时返回null

        #region 从词典中获取布尔值，必须指定默认值

        /// <summary>
        ///     从词典中获取布尔值，必须指定默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public bool GetBool(T key, bool defaultValue)
        {
            var value = GetNullableBool(key);
            if (value.HasValue) return value.Value;

            return defaultValue;
        }

        #endregion 从词典中获取布尔值，必须指定默认值

        #region 从词典中获取nullable布尔值，获取失败时设置为默认值

        /// <summary>
        ///     从词典中获取nullable布尔值，获取失败时设置为默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public bool? GetNullableBool(T key, bool? defaultValue = null)
        {
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            return StringToBool(value.ToString());
        }

        #endregion 从词典中获取nullable布尔值，获取失败时设置为默认值

        #region 从词典中获取小数，如果没有或转换失败则返回default_value

        /// <summary>
        ///     从词典中获取小数，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public decimal GetDecimal(T key, decimal defaultValue = -1)
        {
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            if (!decimal.TryParse(value.ToString(), out var result)) return defaultValue;

            return result;
        }

        #endregion 从词典中获取小数，如果没有或转换失败则返回default_value

        #region 从词典中获取时间，如果没有或转换失败则返回default_value

        /// <summary>
        ///     从词典中获取时间，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public DateTime GetDateTime(T key, DateTime defaultValue = default)
        {
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            if (!DateTime.TryParse(value.ToString(), out var result)) return defaultValue;

            return result;
        }

        #endregion 从词典中获取时间，如果没有或转换失败则返回default_value

        #region 从词典中获取数字列表，如果没有或转换失败则返回default_value

        /// <summary>
        ///     从词典中获取数字列表，如果没有或转换失败则返回default_value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        public IList<int> GetIntList(T key, IList<int> defaultValue = null)
        {
            IList<int> result;
            if (!_dict.TryGetValue(key, out var value)) return defaultValue;

            if (value == null) return null;

            result = StringHelper.StringToIntList(value.ToString());
            return result;
        }

        #endregion 从词典中获取数字列表，如果没有或转换失败则返回default_value

        #region 从指定词典或列表合并，可以选择是否覆盖原来的值

        /// <summary>
        ///     从指定词典或列表合并，可以选择是否覆盖原来的值
        /// </summary>
        /// <param name="pdict"></param>
        /// <param name="replace"></param>
        public void Merge(IEnumerable<KeyValuePair<T, TV>> pdict, bool replace = true)
        {
            foreach (var pair in pdict)
            {
                if (!replace && _dict.ContainsKey(pair.Key)) continue;

                _dict[pair.Key] = pair.Value;
            }
        }

        #endregion 从指定词典或列表合并，可以选择是否覆盖原来的值

        #region 数据定义

        /// <summary>
        ///     要读取的词典对象
        /// </summary>
        private readonly IDictionary<T, TV> _dict;

        /// <summary>
        ///     把字符串转换为指定类型的函数
        /// </summary>
        public delegate TV StringToValue(string str);

        #endregion 数据定义
    }

    #endregion 词典读取器

    #region 转换任意类型的可枚举的对象到词典

    /// <summary>
    ///     转换任意类型的可枚举的对象到词典
    ///     系统类库有一个类似的ToDictionary但不能转换所有类型
    /// </summary>
    /// <typeparam name="S">列表中的对象的类型</typeparam>
    /// <typeparam name="T">词典的键的类型</typeparam>
    /// <typeparam name="V">词典的值的类型</typeparam>
    public static class IterToDictionary<TS, T, TV>
    {
        public delegate T ItemToKey(TS item);

        public delegate TV ItemToValue(TS item);

        public static Dictionary<T, TV> Convert(IEnumerable<TS> list, ItemToKey toKey, ItemToValue toValue)
        {
            var dict = new Dictionary<T, TV>();
            foreach (var item in list) dict[toKey(item)] = toValue(item);

            return dict;
        }
    }

    #endregion 转换任意类型的可枚举的对象到词典

    #region 转换任意类型的可枚举的对象到经过排序的词典

    /// <summary>
    ///     转换任意类型的可枚举的对象到经过排序的词典
    ///     系统类库有一个类似的ToDictionary但不能转换所有类型
    /// </summary>
    /// <typeparam name="S">列表中的对象的类型</typeparam>
    /// <typeparam name="T">词典的键的类型</typeparam>
    /// <typeparam name="V">词典的值的类型</typeparam>
    public static class IterToSortedDictionary<TS, T, TV>
    {
        public delegate T ItemToKey(TS item);

        public delegate TV ItemToValue(TS item);

        public static SortedDictionary<T, TV> Convert(IEnumerable<TS> list, ItemToKey toKey, ItemToValue toValue)
        {
            var dict = new SortedDictionary<T, TV>();
            foreach (var item in list) dict[toKey(item)] = toValue(item);

            return dict;
        }
    }

    #endregion 转换任意类型的可枚举的对象到经过排序的词典
}