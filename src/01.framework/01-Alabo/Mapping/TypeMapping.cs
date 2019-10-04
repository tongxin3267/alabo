using Alabo.Extensions;
using System;
using System.Reflection;

namespace Alabo.Mapping {

    /// <summary>
    ///     Class AutoType.
    ///     类型自动匹配
    /// </summary>
    public static class TypeMapping {

        /// <summary>
        ///     类型转换,转换成Html内容
        ///     如果转换成功，则返回
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The 类型.</param>
        public static Tuple<bool, object> TryChangeHtmlValue(object value, Type type) {
            var changeValue = value;
            if (value == null || value.ToStr().IsNullOrEmpty()) {
                return Tuple.Create(false, changeValue);
            }

            if (type == typeof(string)) {
                changeValue = value.ToStr();
                if (!changeValue.IsNullOrEmpty()) {
                    return Tuple.Create(true, changeValue);
                }
            } else if (type == typeof(int)) {
                if (value.ToStr().ConvertToInt() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(decimal)) {
                if (value.ToStr().ConvertToDecimal() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(long)) {
                if (value.ToStr().ConvertToLong() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(DateTime)) {
                if (value.ToStr().ConvertToDateTime().Year != 1900) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(bool) || type == typeof(bool)) {
                var valueDefault = value.ConvertToNullableBool();
                changeValue = valueDefault != null && (bool)valueDefault ? "是" : "否";
                if (valueDefault.HasValue) {
                    return Tuple.Create(true, changeValue);
                }
            } else if (type == typeof(Guid)) {
                if (!value.ToGuid().IsGuidNullOrEmpty()) {
                    return Tuple.Create(true, value);
                }
            } else if (type.GetTypeInfo().BaseType?.Name == nameof(Enum)) {
                var enumValue = Enum.Parse(type.GetTypeInfo().UnderlyingSystemType,
                    value.IsNullOrEmpty() ? "0" : value.ToStr(), true);
                enumValue = enumValue.GetHtmlName();
                return Tuple.Create(true, enumValue);
            } else {
                return Tuple.Create(false, changeValue);
            }

            return Tuple.Create(false, changeValue);
        }
    }
}