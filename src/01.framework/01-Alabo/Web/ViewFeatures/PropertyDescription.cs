using Alabo.Extensions;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Alabo.Web.ViewFeatures {

    /// <summary>
    ///     获取字段的属性描述信息
    /// </summary>
    public class PropertyDescription {

        public PropertyDescription(Type classType, PropertyInfo property) {
            ClassType = classType;
            Property = property;

            //获取或构建 特性
            FieldAttribute = property.GetAttributes<FieldAttribute>().FirstOrDefault();
            if (FieldAttribute == null) {
                FieldAttribute = new FieldAttribute {
                    //FieldName = property.Name,
                    EditShow = false
                };
            }

            // 通过DataSource来构建FullName
            if (FieldAttribute.DataSource.IsNullOrEmpty()) {
                FieldAttribute.DataSource = FieldAttribute.DataSourceType?.FullName;
            }

            DisplayAttribute = property.GetAttributes<DisplayAttribute>().FirstOrDefault();
            if (DisplayAttribute == null) {
                DisplayAttribute = new DisplayAttribute();
            }

            Name = property.Name;
            HelpBlockAttribute = property.GetAttributes<HelpBlockAttribute>()?.FirstOrDefault();

            RequiredAttribute = property.GetAttributes<RequiredAttribute>()?.FirstOrDefault();

            MaxLengthAttribute = property.GetAttributes<MaxLengthAttribute>()?.FirstOrDefault();

            MinLengthAttribute = property.GetAttributes<MinLengthAttribute>()?.FirstOrDefault();
        }

        public Type ClassType { get; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string Name { get; set; }

        public PropertyInfo Property { get; }

        /// <summary>
        ///     字段特性
        /// </summary>
        public FieldAttribute FieldAttribute { get; }

        /// <summary>
        ///     字段属性特性
        /// </summary>
        public DisplayAttribute DisplayAttribute { get; }

        /// <summary>
        ///     帮助提示信息
        /// </summary>
        public HelpBlockAttribute HelpBlockAttribute { get; set; }

        /// <summary>
        ///     必填属性
        /// </summary>
        public RequiredAttribute RequiredAttribute { get; set; }

        /// <summary>
        ///     最大
        /// </summary>
        public MaxLengthAttribute MaxLengthAttribute { get; set; }

        /// <summary>
        ///     最大
        /// </summary>
        public MinLengthAttribute MinLengthAttribute { get; set; }

        /// <summary>
        ///     将类转换成Json数据
        /// </summary>
        /// <param name="instanse"></param>
        /// <param name="request"></param>
        public static void SetValue(object instanse, object request) {
            foreach (var item in instanse.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                object value = null;
                if (request is HttpRequest) {
                    var obj = (HttpRequest)request;
                    value = obj.Method == "POST" ? obj.Form[item.Name].ToString() : obj.Query[item.Name].ToString();
                }

                if (request is JObject) {
                    var obj = (JObject)request;
                    value = (JValue)obj[item.Name];
                }

                if (request is JValue) {
                    //jvalue 可能是一个json对象，需转换为json对象后再操作。
                    var jvalue = JsonConvert.DeserializeObject(((JValue)request).Value.ToString());
                    var obj = (JObject)jvalue;
                    value = (JValue)obj[item.Name];
                }

                if (item.PropertyType == typeof(string)) {
                    item.SetValue(instanse, value?.ToString());
                } else if (item.PropertyType == typeof(int)) {
                    item.SetValue(instanse, value.ToInt16());
                } else if (item.PropertyType == typeof(bool)) {
                    //Checkbox: bootstrap on 为true
                    if (value != null && (value.ToString().ToLower().Contains("true") ||
                                          value.ToString().ToLower().Contains("on"))) {
                        item.SetValue(instanse, true);
                    } else {
                        item.SetValue(instanse, false);
                    }
                } else if (item.PropertyType == typeof(decimal)) {
                    item.SetValue(instanse, value.ToDecimal());
                } else if (item.PropertyType == typeof(DateTime)) {
                    item.SetValue(instanse, value.ToDateTime());
                } else if (item.PropertyType == typeof(long)) {
                    item.SetValue(instanse, value.ToInt64());
                } else if (item.PropertyType == typeof(byte[])) {
                } else if (item.PropertyType == typeof(Guid)) {
                    item.SetValue(instanse, value.ToGuid());
                } else if (item.PropertyType.Name.Contains("List") || item.PropertyType.Name.Contains("HttpContext")) {
                    //如果List类型，暂时不序列化
                } else {
                    if (item.PropertyType.GetTypeInfo().BaseType.Name == nameof(Enum)) {
                        var enumValue = Enum.Parse(item.PropertyType.GetTypeInfo().UnderlyingSystemType,
                            value.IsNullOrEmpty() ? "0" : value.ToString(), true);

                        item.SetValue(instanse, enumValue);
                    } else {
                        item.SetValue(instanse, value ?? string.Empty);
                    }
                }
            }
        }

        public static List<object> SetValue(object instanse, IList<JObject> jObject) {
            var list = new List<object>();
            foreach (var obj in jObject) {
                var newdata = instanse;
                foreach (var item in instanse.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                    object value = null;
                    if (obj.Property(item.Name) != null) {
                        value = obj.Property(item.Name).Value;
                    }

                    if (item.PropertyType == typeof(string)) {
                        item.SetValue(instanse, value?.ToString());
                    } else if (item.PropertyType == typeof(int)) {
                        item.SetValue(instanse, value.ToInt16());
                    } else if (item.PropertyType == typeof(bool)) {
                        //Checkbox: bootstrap on 为true
                        if (value != null && (value.ToString().ToLower().Contains("true") ||
                                              value.ToString().ToLower().Contains("on"))) {
                            item.SetValue(instanse, true);
                        } else {
                            item.SetValue(instanse, false);
                        }
                    } else if (item.PropertyType == typeof(decimal)) {
                        item.SetValue(instanse, value.ToDecimal());
                    } else if (item.PropertyType == typeof(DateTime)) {
                        item.SetValue(instanse, value.ToDateTime());
                    } else if (item.PropertyType == typeof(long)) {
                        item.SetValue(instanse, value.ToInt64());
                    } else if (item.PropertyType == typeof(Guid)) {
                        item.SetValue(instanse, value.ToGuid());
                    } else if (item.PropertyType.Name.Contains("List") || item.PropertyType.Name.Contains("HttpContext")) {
                        //如果List类型，暂时不序列化
                    } else {
                        // 不可写的, 调用SetValue()会报错
                        if (!item.CanWrite) {
                            continue;
                        }

                        if (item.PropertyType != null && item.PropertyType.GetTypeInfo().BaseType != null &&
                            item.PropertyType.GetTypeInfo().BaseType.Name == nameof(Enum)) {
                            var enumValue = Enum.Parse(item.PropertyType.GetTypeInfo().UnderlyingSystemType,
                                value.IsNullOrEmpty() ? "0" : value.ToString(), true);

                            item.SetValue(instanse, enumValue);
                        } else {
                            item.SetValue(instanse, value ?? string.Empty);
                        }
                    }
                }

                list.Add(instanse);
            }

            return list;
        }

        public static object SetValue(object instanse, JObject obj) {
            var newdata = instanse;
            foreach (var item in instanse.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                object value = null;
                if (obj.Property(item.Name) != null) {
                    value = obj.Property(item.Name).Value;
                }

                if (item.PropertyType == typeof(string)) {
                    item.SetValue(instanse, value?.ToString());
                } else if (item.PropertyType == typeof(int)) {
                    item.SetValue(instanse, value.ToInt16());
                } else if (item.PropertyType == typeof(bool)) {
                    //Checkbox: bootstrap on 为true
                    if (value != null && (value.ToString().ToLower().Contains("true") ||
                                          value.ToString().ToLower().Contains("on"))) {
                        item.SetValue(instanse, true);
                    } else {
                        item.SetValue(instanse, false);
                    }
                } else if (item.PropertyType == typeof(decimal)) {
                    item.SetValue(instanse, value.ToDecimal());
                } else if (item.PropertyType == typeof(DateTime)) {
                    item.SetValue(instanse, value.ToDateTime());
                } else if (item.PropertyType == typeof(long)) {
                    item.SetValue(instanse, value.ToInt64());
                } else if (item.PropertyType == typeof(Guid)) {
                    item.SetValue(instanse, value.ToGuid());
                } else if (item.PropertyType == typeof(byte[])) {
                } else if (item.PropertyType.Name.Contains("List") || item.PropertyType.Name.Contains("HttpContext")) {
                    //如果List类型，暂时不序列化 HttpContext 不序列化
                } else {
                    // 不可写的, 调用SetValue()会报错
                    if (!item.CanWrite) {
                        continue;
                    }

                    if (item.PropertyType != null && item.PropertyType.GetTypeInfo().BaseType != null
                                                  && item.PropertyType.GetTypeInfo().BaseType.Name == nameof(Enum)) {
                        var enumValue = Enum.Parse(item.PropertyType.GetTypeInfo().UnderlyingSystemType,
                            value.IsNullOrEmpty() ? "0" : value.ToString(), true);

                        item.SetValue(instanse, enumValue);
                    } else {
                        item.SetValue(instanse, value ?? string.Empty);
                    }
                }
            }

            return newdata;
        }

        public object GetValue<T>(object instanse) where T : class {
            if (instanse == null) {
                throw new ArgumentNullException(nameof(instanse));
            }

            Func<T, object> getValueFunction = null;
            if (getValueFunction == null) {
                var parameterExpression = Expression.Parameter(typeof(T));
                var convertTypeExpression = Expression.Convert(parameterExpression, ClassType);
                var propertyExpression = Expression.Property(convertTypeExpression, Property);
                var resultConvertExpression = Expression.Convert(propertyExpression, typeof(object));
                var lambdaExpression = Expression.Lambda<Func<T, object>>(resultConvertExpression, parameterExpression);
                getValueFunction = lambdaExpression.Compile();
            }

            return getValueFunction((T)instanse);
        }

        public void SetValue<T>(object instanse, object value) {
            if (instanse == null) {
                throw new ArgumentNullException(nameof(instanse));
            }

            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            Action<T, object> setValueAction = null;
            if (setValueAction == null) {
                var instanseParameterExpression = Expression.Parameter(typeof(T));
                var valueParameterExpression = Expression.Parameter(typeof(object));
                var convertTypeExpression = Expression.Convert(instanseParameterExpression, ClassType);
                var convertValueExpression = Expression.Convert(valueParameterExpression, Property.PropertyType);
                var propertyExpression = Expression.Property(convertTypeExpression, Property);
                var propertyAssginExpression = Expression.Assign(propertyExpression, convertValueExpression);
                var lambdaExpression = Expression.Lambda<Action<T, object>>(propertyAssginExpression,
                    instanseParameterExpression, valueParameterExpression);
                setValueAction = lambdaExpression.Compile();
            }

            setValueAction((T)instanse, value);
        }
    }
}