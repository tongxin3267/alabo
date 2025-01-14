﻿using Alabo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alabo.Helpers {

    /// <summary>
    ///     类型转换
    /// </summary>
    public static class Convert {

        /// <summary>
        ///     转换为32位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static int ToInt(object input) {
            return ToIntOrNull(input) ?? 0;
        }

        /// <summary>
        ///     转换为32位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static int? ToIntOrNull(object input) {
            var success = int.TryParse(input.SafeString(), out var result);
            if (success) {
                return result;
            }

            try {
                var temp = ToDoubleOrNull(input, 0);
                if (temp == null) {
                    return null;
                }

                return System.Convert.ToInt32(temp);
            } catch {
                return null;
            }
        }

        /// <summary>
        ///     转换为64位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long ToLong(object input) {
            return ToLongOrNull(input) ?? 0;
        }

        /// <summary>
        ///     转换为64位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long? ToLongOrNull(object input) {
            var success = long.TryParse(input.SafeString(), out var result);
            if (success) {
                return result;
            }

            try {
                var temp = ToDecimalOrNull(input, 0);
                if (temp == null) {
                    return null;
                }

                return System.Convert.ToInt64(temp);
            } catch {
                return null;
            }
        }

        /// <summary>
        ///     转换为32位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(object input, int? digits = null) {
            return ToFloatOrNull(input, digits) ?? 0;
        }

        /// <summary>
        ///     转换为32位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(object input, int? digits = null) {
            var success = float.TryParse(input.SafeString(), out var result);
            if (!success) {
                return null;
            }

            if (digits == null) {
                return result;
            }

            return (float)Math.Round(result, digits.Value);
        }

        /// <summary>
        ///     转换为64位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(object input, int? digits = null) {
            return ToDoubleOrNull(input, digits) ?? 0;
        }

        /// <summary>
        ///     转换为64位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(object input, int? digits = null) {
            var success = double.TryParse(input.SafeString(), out var result);
            if (!success) {
                return null;
            }

            if (digits == null) {
                return result;
            }

            return Math.Round(result, digits.Value);
        }

        /// <summary>
        ///     转换为128位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(object input, int? digits = null) {
            return ToDecimalOrNull(input, digits) ?? 0;
        }

        /// <summary>
        ///     转换为128位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(object input, int? digits = null) {
            var success = decimal.TryParse(input.SafeString(), out var result);
            if (!success) {
                return null;
            }

            if (digits == null) {
                return result;
            }

            return Math.Round(result, digits.Value);
        }

        /// <summary>
        ///     转换为布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool ToBool(object input) {
            return ToBoolOrNull(input) ?? false;
        }

        /// <summary>
        ///     转换为可空布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool? ToBoolOrNull(object input) {
            var value = GetBool(input);
            if (value != null) {
                return value.Value;
            }

            return bool.TryParse(input.SafeString(), out var result) ? (bool?)result : null;
        }

        /// <summary>
        ///     获取布尔值
        /// </summary>
        private static bool? GetBool(object input) {
            switch (input.SafeString().ToLower()) {
                case "0":
                    return false;

                case "否":
                    return false;

                case "不":
                    return false;

                case "no":
                    return false;

                case "fail":
                    return false;

                case "1":
                    return true;

                case "是":
                    return true;

                case "ok":
                    return true;

                case "yes":
                    return true;

                default:
                    return null;
            }
        }

        /// <summary>
        ///     转换为日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime ToDate(object input) {
            return ToDateOrNull(input) ?? DateTime.MinValue;
        }

        /// <summary>
        ///     转换为可空日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime? ToDateOrNull(object input) {
            return DateTime.TryParse(input.SafeString(), out var result)
                ? (DateTime?)result
                : null;
        }

        /// <summary>
        ///     转换为Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static Guid ToGuid(object input) {
            return ToGuidOrNull(input) ?? Guid.Empty;
        }

        /// <summary>
        ///     转换为可空Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static Guid? ToGuidOrNull(object input) {
            return Guid.TryParse(input.SafeString(), out var result) ? (Guid?)result : null;
        }

        /// <summary>
        ///     转换为Guid集合
        /// </summary>
        /// <param name="input">以逗号分隔的Guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<Guid> ToGuidList(string input) {
            return ToList<Guid>(input);
        }

        /// <summary>
        ///     泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="input">以逗号分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>(string input) {
            var result = new List<T>();
            if (string.IsNullOrWhiteSpace(input)) {
                return result;
            }

            var array = input.Split(',');
            result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select To<T>(each));
            return result;
        }

        /// <summary>
        ///     通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">输入值</param>
        public static T To<T>(object value) {
            if (value == null) {
                return default;
            }

            var type = Common.GetType<T>();
            if (type == typeof(string)) {
                var changeValue = value.ToStr();
                if (!changeValue.IsNullOrEmpty()) {
                    return (T)(object)changeValue;
                }
            } else if (type == typeof(int)) {
                if (value.ToStr().ConvertToInt() != -1) {
                    return (T)(object)value.ToStr().ConvertToInt();
                }
            } else if (type == typeof(decimal)) {
                if (value.ToStr().ConvertToDecimal() != -1) {
                    return (T)(object)value.ToStr().ConvertToDecimal();
                }
            } else if (type == typeof(long)) {
                if (value.ToStr().ConvertToLong() != -1) {
                    return (T)(object)value.ToStr().ConvertToLong();
                }
            } else if (type == typeof(DateTime)) {
                if (value.ToStr().ConvertToDateTime().Year != 1900) {
                    return (T)(object)value.ToStr().ConvertToDateTime();
                }
            } else if (type == typeof(bool) || type == typeof(bool)) {
                var valueDefault = value.ConvertToNullableBool();
                if (valueDefault.HasValue) {
                    return (T)value;
                }
            } else if (type == typeof(Guid)) {
                if (!value.ToGuid().IsGuidNullOrEmpty()) {
                    return (T)(object)value.ToGuid();
                }
            } else if (type.GetTypeInfo().BaseType?.Name == nameof(Enum)) {
                var enumValue = Enum.Parse(type.GetTypeInfo().UnderlyingSystemType,
                    value.IsNullOrEmpty() ? "0" : value.ToStr(), true);
                return Enums.Parse<T>(value);
            } else {
                try {
                    if (value is IConvertible) {
                        return (T)System.Convert.ChangeType(value, type);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    return default;
                }
            }

            return (T)value;
        }

        /// <summary>
        ///     转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>
        public static byte[] ToBytes(string input) {
            return ToBytes(input, Encoding.UTF8);
        }

        /// <summary>
        ///     转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(string input, Encoding encoding) {
            return string.IsNullOrWhiteSpace(input) ? new byte[] { } : encoding.GetBytes(input);
        }
    }
}