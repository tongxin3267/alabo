using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Extensions
{
    /// <summary>
    ///     IEnumerable的扩展函数
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        ///     Foreaches the specified action.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");

            foreach (var item in source) action(item);

            return source;
        }

        /// <summary>
        ///     添加s the range.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="data">The data.</param>
        public static ICollection<T> AddRange<T>(this ICollection<T> source, IEnumerable<T> data)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (data == null) return source;

            foreach (var item in data) source.Add(item);

            return source;
        }

        /// <summary>
        ///     在符合指定条件的元素前添加元素<br />
        ///     如果无元素符合条件则添加到开头<br />
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="before">The before.</param>
        /// <param name="obj">The object.</param>
        public static void AddBefore<T>(this IEnumerable<T> items, Predicate<T> before, T obj)
        {
            var index = items.FindIndex(x => before(x));
            if (index < 0) index = 0;

            items.ToList().Insert(index, obj);
        }

        /// <summary>
        ///     在符合指定条件的元素前添加元素<br />
        ///     如果无元素符合条件则添加到开头<br />
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="before">The before.</param>
        /// <param name="obj">The object.</param>
        public static IEnumerable<T> AddBefore<T>(this IEnumerable<T> items, T obj)
        {
            var list = new List<T>();
            list.Add(obj);
            list.AddRange(items);
            return list;
        }

        /// <summary>
        ///     在符合指定条件的元素后添加元素<br />
        ///     如果无元素符合条件则添加到末尾<br />
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="after">The after.</param>
        /// <param name="obj">The object.</param>
        public static void AddAfter<T>(this IEnumerable<T> items, Predicate<T> after, T obj)
        {
            var index = items.FindLastIndex(x => after(x));
            if (index < 0) index = items.Count() - 1;
        }

        /// <summary>
        ///     判断列表所有的值是否唯一
        /// </summary>
        /// <param name="items"></param>
        public static bool IsDistinct(this IEnumerable<string> items)
        {
            var distinct = items.Distinct();
            if (distinct.Count() < items.Count()) return false;

            return true;
        }

        /// <summary>
        ///     返回第一个符合指定条件的索引<br />
        ///     如果无则返回-1<br />
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">The match.</param>
        public static int FindIndex<T>(this IEnumerable<T> items, int startIndex, Predicate<T> match)
        {
            for (var i = Math.Max(startIndex, 0); i < items.Count(); ++i)
                if (match(items.ToList()[i]))
                    return i;

            return -1;
        }

        /// <summary>
        ///     返回第一个符合指定条件的索引<br />
        ///     如果无则返回-1<br />
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="match">The match.</param>
        public static int FindIndex<T>(this IEnumerable<T> items, Predicate<T> match)
        {
            return FindIndex(items, 0, match);
        }

        /// <summary>
        ///     返回最后一个符合指定条件的索引
        ///     如果无则返回-1
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">The match.</param>
        public static int FindLastIndex<T>(this IEnumerable<T> items, int startIndex, Predicate<T> match)
        {
            for (var i = Math.Min(startIndex, items.Count() - 1); i >= 0; --i)
                if (match(items.ToList()[i]))
                    return i;

            return -1;
        }

        /// <summary>
        ///     返回最后一个符合指定条件的索引<br />
        ///     如果无则返回-1<br />
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="match">The match.</param>
        public static int FindLastIndex<T>(this IEnumerable<T> items, Predicate<T> match)
        {
            return FindLastIndex(items, items.Count() - 1, match);
        }

        /// <summary>
        ///     转换对象列表到由指定符号隔开的数字组字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objList"></param>
        /// <param name="spliter">分隔符</param>
        public static string JoinToString<T>(this IEnumerable<T> objList, string spliter)
        {
            if (objList.IsNullOrEmpty()) return string.Empty;

            if (objList.ToList().Count == 0) return string.Empty;

            return string.Join(spliter, from v in objList where v != null select v.ConvertToString());
        }

        /// <summary>
        ///     Appends the specified source.
        ///     在前面增加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="data">The data.</param>
        public static IEnumerable<T> AppendBefore<T>(this IEnumerable<T> source, T data)
        {
            var list = new List<T>
            {
                data
            };
            list.AddRange(source);
            return list;
        }

        /// <summary>
        ///     Appends the after.
        ///     在后面增加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="data">The data.</param>
        public static IEnumerable<T> AppendAfter<T>(this IEnumerable<T> source, T data)
        {
            var list = new List<T>();
            list.AddRange(source);
            list.Add(data);
            return list;
        }

        /// <summary>
        ///     IEnumerable的Sorted
        /// </summary>
        public static IEnumerable<T> Sorted<T>(this IEnumerable<T> iter)
        {
            return iter.OrderBy(v => v);
        }

        /// <summary>
        ///     判断IEnumerable是否为null或空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        /// <summary>
        ///     获取IEnumerable枚举器
        /// </summary>
        public static IEnumerable<T> GetIter<T>(this IEnumerable enumerable)
        {
            return from T v in enumerable where v is T select v;
        }

        /// <summary>
        ///     转换任意类型的可枚举的对象到词典
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionaryEx<TKey, TValue, TSource>(
            this IEnumerable<TSource> list, Func<TSource, TKey> toKey, Func<TSource, TValue> toValue)
        {
            var dict = new Dictionary<TKey, TValue>();
            foreach (var item in list) dict[toKey(item)] = toValue(item);

            return dict;
        }

        /// <summary>
        ///     转换任意类型的可枚举的对象到经过排序的词典
        /// </summary>
        public static SortedDictionary<TKey, TValue> ToSortedDictionaryEx<TKey, TValue, TSource>(
            this IEnumerable<TSource> list, Func<TSource, TKey> toKey, Func<TSource, TValue> toValue)
        {
            var dict = new SortedDictionary<TKey, TValue>();
            foreach (var item in list) dict[toKey(item)] = toValue(item);

            return dict;
        }

        /// <summary>
        ///     循环获取iter中的对象，并返回一个带有序号的KeyValuePair
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="iter">可枚举的对象</param>
        /// <returns>循环返回带有序号和值的KeyValuePair</returns>
        public static IEnumerable<KeyValuePair<int, T>> IterIndex<T>(this IEnumerable<T> iter)
        {
            var index = 0;
            foreach (var value in iter) yield return new KeyValuePair<int, T>(index++, value);
        }

        /// <summary>
        ///     获取枚举器中所有可能出现的组合
        ///     例：传入 [[1, 2, 3], [1, 2, 3]]
        ///     返回 [[1, 1], [1, 2], [1, 3], [2, 1], [2, 2], [2, 3], [3, 1], [3, 2], [3, 3]]
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="groups">分组</param>
        /// <param name="maxCount"></param>
        /// <returns>组合结果</returns>
        public static T[][] IterCombination<T>(this IEnumerable<IEnumerable<T>> groups, int maxCount = 0x7fffffff)
        {
            //计算可能出现的组合数量
            var count = 1;
            var groupsCount = groups.Count();
            if (groupsCount <= 0) return new T[0][];

            foreach (var set in groups) count *= set.Count();
            //判断是否超过最大允许的组合数量
            if (count > maxCount)
                throw new ArgumentOutOfRangeException("combination count {0} large than max count {1}");
            //确保内存
            var combList = new T[count][];
            for (var _ = 0; _ < count; _++) combList[_] = new T[groupsCount];
            //计算可能出现的组合
            var repat = count; //当前集合中单个值需要的重复次数
            foreach (var pair in groups.IterIndex())
            {
                var setCount = pair.Value.Count(); //当前集合的数量
                repat /= setCount; //更新当前集合中单个值需要的重复次数
                //当前计算的组合位置
                var index = 0;
                for (int min = 0, max = count / repat / setCount; min < max; min++)
                    foreach (var value in pair.Value)
                        for (var _ = 0; _ < repat; _++)
                        {
                            combList[index][pair.Key] = value;
                            index++;
                        }
            }

            //Console.WriteLine(ObjectHelper.ObjectToJson(groups));
            //Console.WriteLine(ObjectHelper.ObjectToJson(comb_list));
            return combList;
        }

        /// <summary>
        ///     同时枚举两个枚举器
        ///     来源：
        ///     http://stackoverflow.com/questions/2427015/how-to-do-pythons-zip-in-c
        /// </summary>
        /// <typeparam name="T">第一个枚举器的类型</typeparam>
        /// <typeparam name="V">第二个枚举器的类型</typeparam>
        /// <param name="a">第一个枚举器</param>
        /// <param name="b">第二个枚举器</param>
        public static IEnumerable<KeyValuePair<T, TV>> IterZip<T, TV>(this IEnumerable<T> a, IEnumerable<TV> b)
        {
            var enumeratorA = a.GetEnumerator();
            var enumeratorB = b.GetEnumerator();
            while (enumeratorA.MoveNext())
            {
                if (!enumeratorB.MoveNext()) yield break;

                yield return new KeyValuePair<T, TV>
                (
                    enumeratorA.Current,
                    enumeratorB.Current
                );
            }
        }
    }
}