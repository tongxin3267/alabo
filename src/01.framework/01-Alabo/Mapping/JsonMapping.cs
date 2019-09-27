using Alabo.Domains.Entities.Core;
using Alabo.Extensions;
using Alabo.Web.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Alabo.Mapping
{
    /// <summary>
    ///     Class JsonMapping.
    ///     Json 数据自动序列化，与反序列化
    /// </summary>
    public static class JsonMapping
    {
        #region 将Json数据转换成实体类型

        /// <summary>
        ///     实体数据转换成扩展数据
        ///     将Json数据转换成实体类型
        ///     如：GetSingle或GetList会自动转换成Json
        ///     Converts to extension.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static T EntityToExtension<T>(T instance) where T : class, IEntity
        {
            if (instance == null) return instance;

            var type = typeof(T);
            var classDescription = new ClassDescription(type);
            //获取有枚举的属性
            var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty());
            if (propertys == null || propertys.Count() == 0) return instance;

            var propertyInfo = type.GetPropertiesFromCache();
            foreach (var item in propertyInfo)
            {
                var property = propertys.FirstOrDefault(r => r.Name == item.Name);

                if (property != null)
                {
                    var value = item.GetValue(instance);
                    if (!value.IsNullOrEmpty())
                    {
                        var extesionName = property.FieldAttribute.ExtensionJson;
                        var extensionField = propertyInfo.FirstOrDefault(r => r.Name == extesionName);
                        if (!extensionField.IsNullOrEmpty())
                            if (extensionField != null)
                            {
                                //设置值
                                var extesionValue =
                                    JsonConvert.DeserializeObject(value.ToString(), extensionField.PropertyType);
                                extensionField.SetValue(instance, extesionValue);
                            }
                    }
                }
            }

            return instance;
        }

        #endregion 将Json数据转换成实体类型

        #region 将Json数据转换成实体类型

        /// <summary>
        ///     将Json数据转换成实体类型
        ///     Converts to extension.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static T ConvertToExtension<T>(T instance) where T : class, IEntity
        {
            if (instance == null) return instance;

            var type = typeof(T);
            var classDescription = new ClassDescription(type);
            if (classDescription.Propertys == null) return instance;
            //获取有枚举的属性
            var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty());
            if (propertys == null || !propertys.Any()) return instance;

            var propertyInfo = type.GetPropertiesFromCache();
            foreach (var item in propertyInfo)
            {
                var property = propertys.FirstOrDefault(r => r.Name == item.Name);

                if (property != null)
                {
                    var value = item.GetValue(instance);
                    if (value != null && !value.ToStr().IsNullOrEmpty())
                    {
                        var extesionName = property.FieldAttribute.ExtensionJson;
                        // 获取扩展字段
                        var extensionType = extesionName.GetTypeByName();
                        var extensionField = propertyInfo.FirstOrDefault(r => r.Name == extensionType?.Name);
                        if (extensionField != null && extensionType != null)
                            try
                            {
                                var extesionValue = JsonConvert.DeserializeObject(value.ToString(), extensionType);
                                extensionField.SetValue(instance, extesionValue);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                    }
                }
            }

            return instance;
        }

        #endregion 将Json数据转换成实体类型

        #region HttpContext表单数据，转换成扩展数据

        /// <summary>
        ///     HTTPs the context to 扩展.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="httpContext">The HTTP context.</param>
        public static T HttpContextToExtension<T>(object instance, HttpContext httpContext)
        {
            instance = HttpContextToExtension(instance, typeof(T), httpContext);
            return (T)instance;
        }

        /// <summary>
        ///     HttpContext表单数据，转换成扩展数据
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="type">The 类型.</param>
        /// <param name="httpContext">The HTTP context.</param>
        public static object HttpContextToExtension(object instance, Type type, HttpContext httpContext)
        {
            // Json
            var classDescription = new ClassDescription(type);
            //获取有枚举的属性
            var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty());
            var propertyInfo = type.GetPropertiesFromCache();
            foreach (var item in propertyInfo)
            {
                var property = propertys.FirstOrDefault(r => r.Name == item.Name);

                if (property != null)
                {
                    var value = httpContext.Request.Form[item.Name].FirstOrDefault();
                    if (!value.IsNullOrEmpty())
                    {
                        var extesionName = property.FieldAttribute.ExtensionJson;
                        var extensionField = propertyInfo.FirstOrDefault(r => r.Name == extesionName);
                        if (!extensionField.IsNullOrEmpty())
                            if (extensionField != null)
                            {
                                //设置值
                                var extesionValue = JsonConvert.DeserializeObject(value, extensionField.PropertyType);
                                extensionField.SetValue(instance, extesionValue);
                            }
                    }
                }
            }

            return instance;
        }

        /// <summary>
        ///     HttpContext表单数据，转换成扩展数据
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="type">The 类型.</param>
        /// <param name="value">The HTTP context.</param>
        public static object HttpContextToExtension(object instance, Type type, string value)
        {
            // Json
            var classDescription = new ClassDescription(type);
            //获取有枚举的属性
            var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty());
            var propertyInfo = type.GetPropertiesFromCache();
            foreach (var item in propertyInfo)
            {
                var property = propertys.FirstOrDefault(r => r.Name == item.Name);

                if (property != null)
                    if (!value.IsNullOrEmpty())
                    {
                        var extesionName = property.FieldAttribute.ExtensionJson;
                        var extensionField = propertyInfo.FirstOrDefault(r => r.Name == extesionName);
                        if (!extensionField.IsNullOrEmpty())
                            if (extensionField != null)
                            {
                                //设置值
                                var extesionValue = JsonConvert.DeserializeObject(value, extensionField.PropertyType);
                                extensionField.SetValue(instance, extesionValue);
                            }
                    }
            }

            return instance;
        }

        #endregion HttpContext表单数据，转换成扩展数据
    }
}