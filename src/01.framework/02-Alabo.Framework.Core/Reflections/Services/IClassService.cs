using System;
using System.Collections.Generic;
using Alabo.Domains.Services;
using Alabo.Web.ViewFeatures;

namespace Alabo.Framework.Core.Reflections.Services
{
    /// <summary>
    ///     类反射相关操作
    /// </summary>
    public interface IClassService : IService
    {
        /// <summary>
        ///     获取类的特性，包括字段属性，字段特性等
        /// </summary>
        /// <param name="fullName">The full name.</param>
        ClassDescription GetClassDescription(string fullName);

        /// <summary>
        ///     获取编辑字段特性
        /// </summary>
        /// <param name="fullName">The full name.</param>
        IEnumerable<PropertyDescription> GetEditPropertys(string fullName);

        /// <summary>
        ///     获取列表页字段特性
        /// </summary>
        /// <param name="fullName"></param>
        IEnumerable<PropertyDescription> GetListPropertys(string fullName);

        /// <summary>
        ///     通过完整的命名空间获取属性值
        /// </summary>
        /// <param name="fullName">输入完整的命名空间</param>
        IEnumerable<PropertyDescription> GetAllPropertys(string fullName);

        /// <summary>
        ///     获取编辑字段特性
        /// </summary>
        /// <param name="type">The full name.</param>
        IEnumerable<PropertyDescription> GetEditPropertys(Type type);

        /// <summary>
        ///     获取列表页字段特性
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<PropertyDescription> GetListPropertys(Type type);

        /// <summary>
        ///     通过完整的命名空间获取属性值
        /// </summary>
        /// <param name="type">输入完整的命名空间</param>
        IEnumerable<PropertyDescription> GetAllPropertys(Type type);
    }
}