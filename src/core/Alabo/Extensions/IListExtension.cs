using System;
using System.Collections.Generic;

namespace Alabo.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        ///     对列表中的对每个元素进行处理
        /// </summary>
        public static IList<T> MapEach<T>(this IList<T> list, Func<T, T> method)
        {
            if (list == null) {
                return list;
            }

            for (var n = 0; n < list.Count; n++) {
                list[n] = method(list[n]);
            }

            return list;
        }

        /// <summary>
        ///     判断列表是否为null或空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> array)
        {
            return array == null || array.Count <= 0;
        }

        /// <summary>
        ///     复制列表，注意这个函数在遇到列表为null或长度溢出时会抛出例外
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="src">来源数组</param>
        /// <param name="srcIndex"></param>
        /// <param name="dst">目标数组</param>
        /// <param name="dstIndex"></param>
        /// <param name="length">要复制的长度</param>
        public static void CopyArray<T>(this IList<T> src, int srcIndex, IList<T> dst, int dstIndex, int length)
        {
            for (var n = 0; n < length; n++) {
                dst[dstIndex++] = src[srcIndex++];
            }
        }

        /// <summary>
        ///     交换位于x和y的对象的位置
        /// </summary>
        public static void Swap<T>(this IList<T> self, int x, int y)
        {
            var tmp = self[x];
            self[x] = self[y];
            self[y] = tmp;
        }

        /// <summary>
        ///     从列表中获取指定位置的值，失败时返回default_value
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="index">位置，负数时为length-index</param>
        /// <param name="defaultValue"></param>
        public static T GetIndex<T>(this IList<T> list, int index, T defaultValue = default)
        {
            if (list == null) {
                return defaultValue;
            }

            if (index < 0) {
                index = list.Count + index;
            }

            if (index < 0 || index >= list.Count) {
                return defaultValue;
            }

            return list[index];
        }

        public static IList<int> ToIntList(this string val)
        {
            var data = val.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var retList = new List<int>();
            data.Foreach(o => retList.Add(o.ToInt()));
            return retList;
        }

        public static IList<long> ToLongList(this string val)
        {
            var data = val.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var retList = new List<long>();
            data.Foreach(o => retList.Add(o.ConvertToLong()));
            return retList;
        }

        /// <summary>
        ///     转换成sql 查询语句
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        public static string ToSqlString(this IList<long> ids)
        {
            var idString = ids.JoinToString("','");
            var result = $"'{idString}'";
            return result;
        }

        /// <summary>
        ///     转换成sql 查询语句
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        public static string ToSqlString(this IList<object> ids)
        {
            var idString = ids.JoinToString("','");
            var result = $"'{idString}'";
            return result;
        }

        /// <summary>
        ///     转换成sql 查询语句
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        public static string ToSqlString(this IList<Guid> ids)
        {
            var idString = ids.JoinToString("','");
            var result = $"'{idString}'";
            return result;
        }
    }
}