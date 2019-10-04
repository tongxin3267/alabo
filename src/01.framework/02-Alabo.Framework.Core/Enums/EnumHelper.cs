using Alabo.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.Framework.Core.Enums {

    public class EnumHelper {

        /// <summary>
        ///     将 int 枚举类型转换为 值与枚举名 键值对字典
        /// </summary>
        /// <param name="enumType">枚举类型，如果不是 int 枚举类型将抛出异常</param>
        public static Dictionary<int, string> GetIntDictionary(Type enumType) {
            var dict = new Dictionary<int, string>();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                dict.Add((int)v, System.Enum.GetName(enumType, v));
            }

            return dict;
        }

        public static Dictionary<long, string> GetLongDisplayName(Type enumType) {
            var dict = new Dictionary<long, string>();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                dict.Add((long)v, v.GetDisplayName());
            }

            return dict;
        }

        /// <summary>
        ///     将 long 枚举类型转换为 值与枚举名 键值对字典
        /// </summary>
        /// <param name="enumType">枚举类型，如果不是 long 枚举类型将抛出异常</param>
        public static Dictionary<long, string> GetLongDictionary(Type enumType) {
            var dict = new Dictionary<long, string>();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                dict.Add((long)v, System.Enum.GetName(enumType, v));
            }

            return dict;
        }

        /// <summary>
        ///     将 short 枚举类型转换为 值与枚举名 键值对字典
        /// </summary>
        /// <param name="enumType">枚举类型，如果不是 short 枚举类型将抛出异常</param>
        public static Dictionary<short, string> GetShortDictionary(Type enumType) {
            var dict = new Dictionary<short, string>();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                dict.Add((short)v, System.Enum.GetName(enumType, v));
            }

            return dict;
        }

        /// <summary>
        ///     将枚举类型转换为 值与枚举名 键值对字典
        /// </summary>
        /// <param name="enumType">枚举类型，如果不是枚举类型将抛出异常</param>
        public static Dictionary<object, string> GetDictionary(Type enumType) {
            var dict = new Dictionary<object, string>();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                dict.Add(v, System.Enum.GetName(enumType, v));
            }

            return dict;
        }

        /// <summary>
        ///     构建 select 选项
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="selectedValue"></param>
        public static string BuildOptions(Type enumType, long selectedValue) {
            var sb = new StringBuilder();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                if (v.ToString() == selectedValue.ToString()) {
                    sb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", v,
                        System.Enum.GetName(enumType, v));
                } else {
                    sb.AppendFormat("<option value='{0}'>{1}</option>", v, v.GetDisplayName());
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     构建 select 选项
        /// </summary>
        /// <param name="enumType"></param>
        public static string BuildOptions<T>(Type enumType) {
            var sb = new StringBuilder();
            var arr = System.Enum.GetValues(enumType);
            foreach (var v in arr) {
                sb.AppendFormat("<option value='{0}'>{1}</option>", (T)v, v.GetDisplayName());
            }

            return sb.ToString();
        }
    }
}