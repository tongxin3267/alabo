using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Web.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Framework.Core.Reflections.Services {

    /// <summary>
    ///     Class CalssService.
    /// </summary>
    public class ClassService : ServiceBase, IClassService {

        public ClassService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     获取类的特性，包括字段属性，字段特性等
        /// </summary>
        /// <param name="fullName">The full name.</param>
        public ClassDescription GetClassDescription(string fullName) {
            return fullName.GetClassDescription();
        }

        /// <summary>
        ///     获取编辑字段特性
        /// </summary>
        /// <param name="fullName">The full name.</param>
        public IEnumerable<PropertyDescription> GetEditPropertys(string fullName) {
            var propertys = GetAllPropertys(fullName);
            return propertys?.Where(r => r.FieldAttribute.EditShow);
        }

        /// <summary>
        ///     获取列表页字段特性
        /// </summary>
        /// <param name="fullName">The full name.</param>
        public IEnumerable<PropertyDescription> GetListPropertys(string fullName) {
            var propertys = GetAllPropertys(fullName);
            return propertys?.Where(r => r.FieldAttribute.ListShow);
        }

        /// <summary>
        ///     通过完整的命名空间获取属性值
        /// </summary>
        /// <param name="fullName">输入完整的命名空间</param>
        public IEnumerable<PropertyDescription> GetAllPropertys(string fullName) {
            try {
                var t = fullName.GetTypeByFullName();
                var configDescription = new ClassDescription(t);
                return configDescription.Propertys.ToList();
            } catch {
                return null;
            }
        }

        /// <summary>
        ///     获取编辑字段特性
        /// </summary>
        /// <param name="type">The full name.</param>
        public IEnumerable<PropertyDescription> GetEditPropertys(Type type) {
            var configDescription = new ClassDescription(type);
            return configDescription.Propertys.Where(r => r.FieldAttribute.EditShow).ToList();
        }

        /// <summary>
        ///     获取列表页字段特性
        /// </summary>
        /// <param name="type">The 类型.</param>
        public IEnumerable<PropertyDescription> GetListPropertys(Type type) {
            var configDescription = new ClassDescription(type);
            return configDescription.Propertys.Where(r => r.FieldAttribute.ListShow).ToList();
        }

        /// <summary>
        ///     通过完整的命名空间获取属性值
        /// </summary>
        /// <param name="type">输入完整的命名空间</param>
        public IEnumerable<PropertyDescription> GetAllPropertys(Type type) {
            var configDescription = new ClassDescription(type);
            return configDescription.Propertys.ToList();
        }
    }
}