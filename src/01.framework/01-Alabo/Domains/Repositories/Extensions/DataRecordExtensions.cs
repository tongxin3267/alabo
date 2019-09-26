using System;
using System.Data;
using Alabo.Domains.Enums;

namespace Alabo.Domains.Repositories.Extensions
{
    /// <summary>
    ///     数据库操作扩展
    /// </summary>
    public static class DataRecordExtension
    {
        static DataRecordExtension()
        {
            Cache<byte>.Invoker = (reader, i) => reader.GetByte(i);
            Cache<short>.Invoker = (reader, i) => reader.GetInt16(i);
            Cache<int>.Invoker = (reader, i) => reader.GetInt32(i);
            Cache<long>.Invoker = (reader, i) => reader.GetInt64(i);
            Cache<string>.Invoker = (reader, i) => reader.GetString(i);
            Cache<object>.Invoker = (reader, i) => reader.GetValue(i);
            Cache<bool>.Invoker = (reader, i) => reader.GetBoolean(i);
            Cache<char>.Invoker = (reader, i) => reader.GetChar(i);
            Cache<DateTime>.Invoker = (reader, i) => reader.GetDateTime(i);
            Cache<decimal>.Invoker = (reader, i) => reader.GetDecimal(i);
            Cache<double>.Invoker = (reader, i) => reader.GetDouble(i);
            Cache<float>.Invoker = (reader, i) => reader.GetFloat(i);
            Cache<Guid>.Invoker = (reader, i) => reader.GetGuid(i);
        }

        /// <summary>
        ///     从DataRecord中快速读取指定类型的数据
        ///     此方法永远不会抛出异常
        ///     当读取的对象为缓存定义的类型，读取效率最高，若读取失败，返回default(T)
        ///     当读取的对象为非缓存定义类型，则使用数据强制转换完成，若类型转换失败，不会抛出异常，直接返回default(T)
        /// </summary>
        /// <typeparam name="T">需要读取的数据类型</typeparam>
        /// <param name="reader">datarecord对象</param>
        /// <param name="i">列位置</param>
        /// <returns>所需的T对象</returns>
        public static T Read<T>(this IDataRecord reader, int i)
        {
            var invoker = Cache<T>.Invoker;
            if (invoker != null)
                try
                {
                    return invoker(reader, i);
                }
                catch
                {
                    return default;
                }

            var objectValue = reader.GetValue(i);
            try
            {
                return (T) objectValue;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     从DataRecord中快速读取指定类型的数据
        ///     此方法先将name转化为Ordinal，再调用重载的Read方法
        /// </summary>
        /// <typeparam name="T">需要读取的数据类型</typeparam>
        /// <param name="reader">datarecord对象</param>
        /// <param name="name">列名称</param>
        /// <returns>所需的T对象</returns>
        public static T Read<T>(this IDataRecord reader, string name)
        {
            var i = reader.GetOrdinal(name);
            return Read<T>(reader, i);
        }

        public static T TryRead<T>(this IDataRecord reader, string name, T defaultValue = default)
        {
            try
            {
                var i = reader.GetOrdinal(name);
                return Read<T>(reader, i);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T SafeRead<T>(this IDataRecord reader, string name)
        {
            return reader.TryRead<T>(name);
        }

        /// <summary>
        ///     根据timeType获取条件,获取季度，年度、月份等条件
        ///     http://www.w3school.com.cn/sql/func_datepart.asp
        ///     https://www.cnblogs.com/hyd1213126/p/5828464.html
        /// </summary>
        /// <param name="tiemType"></param>
        /// <param name="dateTime"></param>
        public static string GetTimeTypeCondition(TimeType tiemType, DateTime dateTime)
        {
            var sqlWhere = string.Empty;
            switch (tiemType)
            {
                // 当天
                case TimeType.Day:
                    sqlWhere = $"datediff(day,CreateTime,'{dateTime}')=0";
                    break;
                // 当周
                case TimeType.Week:
                    //sqlWhere = $" (DATEPART(yy,CreateTime) = DATEPART(yy,'{dateTime}')) AND (DATEPART(wk, CreateTime) = DATEPART(wk, '{dateTime}'))";
                    sqlWhere = $"datediff(week,CreateTime,'{dateTime}')=0";
                    break;
                // 当月
                case TimeType.Month:
                    // sqlWhere = $" (DATEPART(yy, CreateTime) = DATEPART(yy,'{dateTime}')) AND (DATEPART(mm, CreateTime) = DATEPART(mm, '{dateTime}'))";
                    sqlWhere = $"datediff(month,CreateTime,'{dateTime}')=0";
                    break;
                // 当季度
                case TimeType.Quarter:
                    //sqlWhere = $" (DATEPART(yy,CreateTime) = DATEPART(yy,'{dateTime}')) AND (DATEPART(qq, CreateTime) = DATEPART(qq, '{dateTime}'))";
                    sqlWhere = $"datediff(qq,CreateTime,'{dateTime}')=0";
                    break;
                // 当年
                case TimeType.Year:
                    //sqlWhere = $" (DATEPART(yy,CreateTime) = DATEPART(yy,'{dateTime}')) AND (DATEPART(yy, CreateTime) = DATEPART(yy, '{dateTime}'))";
                    sqlWhere = $"datediff(year,CreateTime,'{dateTime}')=0";
                    break;

                // 当半年(前半年1-6，后半年7-12）
                case TimeType.HalfYear:
                    sqlWhere =
                        $" (DATEPART(yy,CreateTime) = DATEPART(yy,'{dateTime}')) AND  datepart(mm,'{dateTime}')/7 = datepart(mm,CreateTime)/7";
                    break;
                // 当小时
                case TimeType.Hours:
                    sqlWhere =
                        $" (DATEPART(yy,CreateTime) = DATEPART(yy,'{dateTime}')) AND (DATEPART(hh, CreateTime) = DATEPART(hh, '{dateTime}')) " +
                        $" AND (DATEPART(mm, CreateTime) = DATEPART(mm, '{dateTime}'))" +
                        $" AND (DATEPART(dd, CreateTime) = DATEPART(dd, '{dateTime}'))";
                    break;
                // 当分钟
                case TimeType.Minute:
                    sqlWhere =
                        $" (DATEPART(yy,CreateTime) = DATEPART(yy,'{dateTime}')) AND (DATEPART(mi, CreateTime) = DATEPART(mi, '{dateTime}'))" +
                        $" AND (DATEPART(mm, CreateTime) = DATEPART(mm, '{dateTime}'))" +
                        $" AND (DATEPART(dd, CreateTime) = DATEPART(dd, '{dateTime}'))" +
                        $" AND (DATEPART(hh, CreateTime) = DATEPART(hh, '{dateTime}'))";
                    break;
            }

            return sqlWhere;
        }

        private static class Cache<T>
        {
            public static Func<IDataRecord, int, T> Invoker;
        }
    }
}