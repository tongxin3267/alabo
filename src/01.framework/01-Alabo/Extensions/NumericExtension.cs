using System;

namespace Alabo.Extensions
{
    public static class NumericExtension
    {
        public static string ToPercent(this decimal value, int place = 2)
        {
            if (value > 1 || value < 0) {
                return string.Empty;
            }

            return (value * 100).ToString($"f{place}") + "%";
        }

        /// <summary>
        ///     获取绝对值
        /// </summary>
        /// <param name="v">数值</param>
        public static int Abs(this int v)
        {
            return v < 0 ? -v : v;
        }

        /// <summary>
        ///     获取绝对值
        /// </summary>
        /// <param name="v">数值</param>
        public static long Abs(this long v)
        {
            return v < 0 ? -v : v;
        }

        /// <summary>
        ///     获取绝对值
        /// </summary>
        /// <param name="v">数值</param>
        public static float Abs(this float v)
        {
            return v < 0 ? -v : v;
        }

        /// <summary>
        ///     获取绝对值
        /// </summary>
        /// <param name="v">数值</param>
        public static double Abs(this double v)
        {
            return v < 0 ? -v : v;
        }

        /// <summary>
        ///     获取绝对值
        /// </summary>
        public static decimal Abs(this decimal v)
        {
            return v < 0 ? -v : v;
        }

        /// <summary>
        ///     约束数值在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static int Restrict(this int v, int minValue, int maxValue)
        {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException("min_value cannot large than max_value");
            }

            return Math.Max(Math.Min(v, maxValue), minValue);
        }

        /// <summary>
        ///     约束数值在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static long Restrict(this long v, long minValue, long maxValue)
        {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException("min_value cannot large than max_value");
            }

            return Math.Max(Math.Min(v, maxValue), minValue);
        }

        /// <summary>
        ///     约束数值在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static float Restrict(this float v, float minValue, float maxValue)
        {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException("min_value cannot large than max_value");
            }

            return Math.Max(Math.Min(v, maxValue), minValue);
        }

        /// <summary>
        ///     约束数值在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static double Restrict(this double v, double minValue, double maxValue)
        {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException("min_value cannot large than max_value");
            }

            return Math.Max(Math.Min(v, maxValue), minValue);
        }

        /// <summary>
        ///     约束数值在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static decimal Restrict(this decimal v, decimal minValue, decimal maxValue)
        {
            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException("min_value cannot large than max_value");
            }

            return Math.Max(Math.Min(v, maxValue), minValue);
        }

        /// <summary>
        ///     检查数值是否在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static bool IsRestricted(this int v, int minValue, int maxValue)
        {
            return v >= minValue && v <= maxValue;
        }

        /// <summary>
        ///     检查数值是否在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static bool IsRestricted(this long v, long minValue, long maxValue)
        {
            return v >= minValue && v <= maxValue;
        }

        /// <summary>
        ///     检查数值是否在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static bool IsRestricted(this float v, float minValue, float maxValue)
        {
            return v >= minValue && v <= maxValue;
        }

        /// <summary>
        ///     检查数值是否在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static bool IsRestricted(this double v, double minValue, double maxValue)
        {
            return v >= minValue && v <= maxValue;
        }

        /// <summary>
        ///     检查数值是否在指定范围内
        /// </summary>
        /// <param name="v">数值</param>
        /// <param name="min_value">最小值（包含最小值）</param>
        /// <param name="max_value">最大值（包含最大值）</param>
        public static bool IsRestricted(this decimal v, decimal minValue, decimal maxValue)
        {
            return v >= minValue && v <= maxValue;
        }

        /// <summary>
        ///     获取保留的指定小数点位数的字符串
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="retain">要保留的小数点位置</param>
        public static string ToString(this decimal value, int retain)
        {
            return string.Format("{0:F" + retain + "}", value);
        }

        /// <summary>
        ///     获取保留的指定小数点位数的decimal，四舍五入
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="retain">要保留的小数点位置</param>
        public static decimal RoundCn(this decimal value, int retain)
        {
            return Math.Round(value, retain, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///     获取保留的指定小数点位数的decimal，总是舍去后面的小数
        ///     Math.Floor不支持指定保留的小数点位数
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="retain">要保留的小数点位置</param>
        public static decimal RoundDown(this decimal value, int retain)
        {
            var result = Math.Round(value, retain, MidpointRounding.AwayFromZero);
            if (result > value) {
                result -= (decimal)Math.Pow(10, -retain); //2 => 0.01
            }

            return result;
        }

        /// <summary>
        ///     转换int到enum，失败时返回default_value
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="value">int值</param>
        /// <param name="default_value">失败时返回的默认值</param>
        public static T ConvertToEnum<T>(this int value, T defaultValue) where T : struct, IConvertible
        {
            if (!Enum.IsDefined(typeof(T), value)) {
                return defaultValue;
            }

            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>
        ///     转换int到enum，失败时返回null
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="value">int值</param>
        /// <param name="default_value">失败时返回的默认值</param>
        public static T? ConvertToNullableEnum<T>(this int value) where T : struct, IConvertible
        {
            if (!Enum.IsDefined(typeof(T), value)) {
                return null;
            }

            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}