using Alabo.Cache;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZKCloud.Open.DynamicExpression;

namespace Alabo.Web.ViewFeatures
{
    /// <summary>
    ///     获取类的属性以及描述信息
    /// </summary>
    public class ClassDescription
    {
        /// <summary>
        ///     The cache
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ClassDescription> Cache =
            new ConcurrentDictionary<Type, ClassDescription>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClassDescription" /> class.
        /// </summary>
        public ClassDescription()
        {
            ClassPropertyAttribute = new ClassPropertyAttribute();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClassDescription" /> class.
        /// </summary>
        /// <param name="configType">The configuration 类型.</param>
        public ClassDescription(Type configType)
        {
            Init(configType);
        }

        public ClassDescription(string fullName)
        {
            var configType = fullName.GetTypeByFullName();
            Init(configType);
        }

        /// <summary>
        ///     类型
        /// </summary>
        public Type ClassType { get; private set; }

        /// <summary>
        ///     类特性
        /// </summary>
        public ClassPropertyAttribute ClassPropertyAttribute { get; private set; }

        /// <summary>
        ///     类所有字段，或者所有属性
        /// </summary>
        public PropertyDescription[] Propertys { get; private set; }

        /// <summary>
        ///     表格、表单、列快捷操作链接
        ///     方法名必须为：ViewLinks() ，或者动态获取不到
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks { get; set; }

        private Type GetGenericType(Type configType)
        {
            if (configType.IsGenericType)
            {
                // 如果是泛型类型获取基类类型
                var result = configType.GenericTypeArguments[0];
                return result;
            }

            return configType;
        }

        private void Init(Type configType)
        {
            var baseType = GetGenericType(configType);
            var objectCache = Ioc.Resolve<IObjectCache>();
            var cacheKey = $"classDescription_{baseType.FullName.Replace(".", "_")}";
            if (!objectCache.TryGet(cacheKey, out CacheClassDescription cacheDescription))
            {
                cacheDescription = Create(baseType);
                if (cacheDescription != null) objectCache.Set(cacheKey, cacheDescription);
            }

            if (cacheDescription != null)
            {
                ClassType = baseType;
                ClassPropertyAttribute = cacheDescription.ClassPropertyAttribute;
                Propertys = cacheDescription.Propertys;
                ViewLinks = cacheDescription.ViewLinks;
            }
        }

        /// <summary>
        ///     Creates the specified configuration 类型.
        /// </summary>
        /// <param name="configType">The configuration 类型.</param>
        public CacheClassDescription Create(Type configType)
        {
            var objectCache = Ioc.Resolve<IObjectCache>();
            var cacheKey = $"classDescription_{configType.FullName.Replace(".", "_")}";

            if (!objectCache.TryGet(cacheKey, out CacheClassDescription cacheDescription))
            {
                var classType = configType ?? throw new ArgumentNullException(nameof(configType));

                var classPropertyAttribute =
                    classType.GetTypeInfo().GetAttributes<ClassPropertyAttribute>().FirstOrDefault();
                //如果类特性为空，配置特性
                if (classPropertyAttribute == null)
                {
                    var typeName = classType.Name;
                    classPropertyAttribute = new ClassPropertyAttribute();
                    classPropertyAttribute.Name = typeName;
                }

                //字段特性
                var propertys = classType.GetProperties().Select(e => new PropertyDescription(e.DeclaringType, e))
                    .ToArray();
                propertys = propertys.OrderBy(r => r.FieldAttribute.SortOrder).ToArray();

                //快捷操作链接
                var links = new List<ViewLink>();

                var linkMethod = configType.GetMethod("ViewLinks");
                if (linkMethod != null)
                {
                    // 使用动态方法获取链接地址
                    var config = Activator.CreateInstance(configType);
                    var target = new Interpreter().SetVariable("baseViewModel", config);
                    links = (List<ViewLink>)target.Eval("baseViewModel.ViewLinks()");
                }

                cacheDescription = new CacheClassDescription
                {
                    ClassType = classType,
                    ClassPropertyAttribute = classPropertyAttribute,
                    Propertys = propertys,
                    ViewLinks = links
                };

                objectCache.Set(cacheKey, cacheDescription);
            }

            return cacheDescription;
        }
    }

    /// <summary>
    ///     Class CacheClassDescription.
    /// </summary>
    public class CacheClassDescription
    {
        /// <summary>
        ///     类型
        /// </summary>
        internal Type ClassType { get; set; }

        /// <summary>
        ///     类特性
        /// </summary>
        internal ClassPropertyAttribute ClassPropertyAttribute { get; set; }

        /// <summary>
        ///     类所有字段，或者所有属性
        /// </summary>
        internal PropertyDescription[] Propertys { get; set; }

        /// <summary>
        ///     表格、表单、列快捷操作链接
        ///     方法名必须为：ViewLinks() ，或者动态获取不到
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks { get; set; }
    }
}