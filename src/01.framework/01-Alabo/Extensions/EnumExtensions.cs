using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Cache;
using Alabo.Exceptions;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Convert = System.Convert;

namespace Alabo.Extensions
{
    /// <summary>
    ///     枚举类型的扩展函数
    /// </summary>
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string enumString)
        {
            return (T) Enum.Parse(typeof(T), enumString);
        }

        public static Dictionary<string, object> GetEnumDictionary<T>()
        {
            var keyValuePairs = new Dictionary<string, object>();
            foreach (Enum item in Enum.GetValues(typeof(T)))
            {
                var value = item.GetDisplayName();
                var key = Convert.ToInt16(item);
                keyValuePairs.Add(key.ToString(), value);
            }

            return keyValuePairs;
        }

        /// <summary>
        ///     获取枚举属性值
        /// </summary>
        public static IEnumerable<SelectListItem> ToSelectListItem<T>() where T : struct, IConvertible
        {
            return (from int value in Enum.GetValues(typeof(T))
                select new SelectListItem
                {
                    Text = Enum.GetName(typeof(T), value),
                    Value = value.ToString()
                }).ToList();
        }

        /// <summary>
        ///     获取枚举属性值，并设置默认值
        /// </summary>
        /// <param name="selectName">默认值</param>
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(string selectName) where T : struct, IConvertible
        {
            return (from int value in Enum.GetValues(typeof(T))
                select new SelectListItem
                {
                    Text = Enum.GetName(typeof(T), value),
                    Value = Enum.GetName(typeof(T), value),
                    Selected = Enum.GetName(typeof(T), value) == selectName ? true : false
                }).ToList();
        }

        /// <summary>
        ///     获取枚举值的显示名称
        ///     有指定Display属性时返回该属性的对应名称，否则返回字段本身的名称
        /// </summary>
        /// <param name="value">枚举值</param>
        public static string GetDisplayName(this Enum value)
        {
            // 获取枚举值类型和名称
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return Convert.ToInt32(value).ToString();
            // 获取Display属性
            var field = type.GetField(Enum.GetName(type, value));
            var displayAttribute = field.GetAttributes<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute != null) return displayAttribute.GetName() ?? displayAttribute.GetShortName();

            // 返回默认名称
            return name;
        }

        /// <summary>
        ///     GetDisplayResourceTypeName
        /// </summary>
        /// <param name="value">枚举值</param>
        public static string GetDisplayResourceTypeName(this Enum value)
        {
            var displayAttribute = GetDisplayAttribute(value);
            if (displayAttribute != null && displayAttribute.ResourceType != null)
                return displayAttribute.ResourceType.FullName;

            return string.Empty;
        }

        /// <summary>
        ///     获取枚举值Display项的值
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="selector">选择Display返回值</param>
        public static string GetDisplayInfo(this Enum value, Func<DisplayAttribute, string> selector)
        {
            // 获取枚举值类型和名称
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return Convert.ToInt32(value).ToString();
            // 获取Display属性
            var field = type.GetField(Enum.GetName(type, value));
            var displayAttribute = field.GetAttributes<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute != null) return selector(displayAttribute);

            // 返回默认名称
            return name;
        }

        private static DisplayAttribute GetDisplayAttribute(Enum value)
        {
            //get enum value
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return null;
            //field
            var field = type.GetField(name);
            return field.GetAttributes<DisplayAttribute>().FirstOrDefault();
        }

        /// <summary>
        ///     根据枚举获取字段配置特性
        /// </summary>
        /// <param name="value">The value.</param>
        public static FieldAttribute GetFieldAttribute(this Enum value)
        {
            var type = value.GetType();
            var objectCache = Ioc.Resolve<IObjectCache>();
            return objectCache.GetOrSet(() =>
                {
                    var field = type.GetField(Enum.GetName(type, value));
                    var fieldAttribute = field.GetAttribute<FieldAttribute>();
                    return fieldAttribute;
                }, type.FullName + "_Field_" + value.ToStr()).Value;
        }

        /// <summary>
        ///     根据枚举获取字段
        ///     Guid
        /// </summary>
        /// <param name="value">The value.</param>
        public static Guid GetFieldId(this Enum value)
        {
            // 获取枚举值类型和名称
            var fieldAttribute = GetFieldAttribute(value);
            if (fieldAttribute == null) return Guid.Empty;

            try
            {
                return Guid.Parse(fieldAttribute.GuidId);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        ///     获取图标
        /// </summary>
        /// <param name="value">The value.</param>
        public static string GetIcon(this Enum value)
        {
            // 获取枚举值类型和名称
            var fieldAttribute = GetFieldAttribute(value);
            if (fieldAttribute == null) return string.Empty;

            return fieldAttribute.Icon?.Replace("_", "_");
        }

        /// <summary>
        ///     获取s the custom attribute.
        /// </summary>
        /// <param name="value">The value.</param>
        public static T GetCustomAttr<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var field = type.GetField(Enum.GetName(type, value));
            var t = field.GetAttribute<T>();
            return t;
        }

        /// <summary>
        ///     获取s the display name.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string GetDisplayName(this object value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return Convert.ToInt32(value).ToString();
            // 获取Display属性
            var field = type.GetField(Enum.GetName(type, value));
            var displayAttribute = field.GetAttributes<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute != null) return displayAttribute.Name ?? displayAttribute.ShortName;
            // 返回默认名称
            return name;
        }

        /// <summary>
        ///     是否为默认属性
        /// </summary>
        /// <param name="value">The value.</param>
        public static bool IsDefault(this Enum value)
        {
            // 获取枚举值类型和名称
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return false;
            // 获取Display属性
            var field = type.GetField(Enum.GetName(type, value));
            var fieldAttribute = field.GetAttributes<FieldAttribute>().FirstOrDefault();
            if (fieldAttribute != null) return fieldAttribute.IsDefault;
            // 返回默认名称
            return false;
            ;
        }

        /// <summary>
        ///     获取HTML显示格式
        /// </summary>
        /// <param name="value">枚举值</param>
        public static string GetHtmlName(this object value)
        {
            // 获取枚举值类型和名称
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return Convert.ToInt32(value).ToString();
            // 获取Display属性
            var field = type.GetField(Enum.GetName(type, value));
            var displayAttribute = field.GetAttributes<DisplayAttribute>().FirstOrDefault();

            if (displayAttribute != null)
            {
                name = displayAttribute.GetName() ?? displayAttribute.GetShortName();
                var cssAttribute = field.GetAttributes<LabelCssClassAttribute>().FirstOrDefault();
                if (cssAttribute != null)
                    name = $@"<span class='m-badge m-badge--wide {cssAttribute.CssClass}'>{name}</span>";
            }

            // 返回默认名称
            return name;
        }

        /// <summary>
        ///     获取s the name of the HTML.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string GetHtmlName(this Enum value)
        {
            // 获取枚举值类型和名称
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return Convert.ToInt32(value).ToString();
            // 获取Display属性
            var field = type.GetField(Enum.GetName(type, value));
            var displayAttribute = field.GetAttributes<DisplayAttribute>().FirstOrDefault();

            if (displayAttribute != null)
            {
                name = displayAttribute.GetName() ?? displayAttribute.GetShortName();
                var cssAttribute = field.GetAttributes<LabelCssClassAttribute>().FirstOrDefault();
                if (cssAttribute != null)
                    name = $@"<span class='m-badge  m-badge--wide {cssAttribute.CssClass}'>{name}</span>";
            }

            // 返回默认名称
            return name;
        }

        /// <summary>
        ///     将字符串转为枚举，先判断是否成功，如果成功则返回枚举值
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <param name="enumValue">The enum value.</param>
        public static bool StringToEnum<T>(this string stringValue, out T enumValue)
        {
            try
            {
                enumValue = (T) Enum.Parse(typeof(T), stringValue, true);
                if (Enum.IsDefined(typeof(T), enumValue)) return true;

                return false;
            }
            catch
            {
                enumValue = default;
                return false;
            }
        }

        /// <summary>
        ///     将数值转为枚举，先判断是否成功，如果成功则返回枚举值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="enumValue">The enum value.</param>
        public static bool IntToEnum<T>(this int value, out T enumValue)
        {
            try
            {
                if (Enum.IsDefined(typeof(T), value))
                {
                    enumValue = (T) Enum.ToObject(typeof(T), value);
                    return true;
                }

                enumValue = default;
                return false;
            }
            catch
            {
                enumValue = default;
                return false;
            }
        }

        /// <summary>
        ///     判断枚举是否值，是否安全，值是否重复
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static bool IsSafe<T>()
        {
            var result = true;
            var list = new List<int>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var value = Convert.ToInt16(item);
                if (list.Contains(value))
                {
                    result = false;
                    throw new ValidException("枚举值定义重复");
                }

                list.Add(value);
            }

            return result;
        }
    }
}