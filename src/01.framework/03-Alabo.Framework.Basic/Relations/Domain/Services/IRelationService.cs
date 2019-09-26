using System;
using System.Collections.Generic;
using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IRelationService : IService<Relation, long> {

        /// <summary>
        ///     检查分类是否存在
        /// </summary>
        /// <param name="script"></param>
        bool CheckExist(string script);

        /// <summary>
        ///     获取所有的级联类型
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        /// <summary>
        ///     获取所有的分类类型
        /// </summary>
        IEnumerable<Type> GetAllClassTypes();

        /// <summary>
        ///     获取所有的标签类型
        /// </summary>
        IEnumerable<Type> GetAllTagTypes();

        /// <summary>
        ///     根据类型获取所有的分类
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<RelationApiOutput> GetClass(string type);

        /// <summary>
        ///     根据类型获取所有的分类
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<RelationApiOutput> GetClass(string type, long userId);

        /// <summary>
        ///     根据类型获取所有的父级分类
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<RelationApiOutput> GetFatherClass(string type);

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<KeyValue> GetFatherKeyValues(string type);

        /// <summary>
        ///     根据类型获取所有的标签
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<RelationApiOutput> GetTag(string type);

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<KeyValue> GetKeyValues(string type);

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类
        ///     和上面的方法一样，只是key和value的方式不一样，调换下
        /// </summary>
        /// <param name="type"></param>
        IEnumerable<KeyValue> GetKeyValues2(string type);

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<KeyValue> GetKeyValues(string type, long userId);

        /// <summary>
        ///     查找类型
        /// </summary>
        /// <param name="typeName"></param>
        Type FindType(string typeName);

        /// <summary>
        /// 获取所有分类的链接地址
        /// </summary>
        /// <returns></returns>
        List<Link> GetClassLinks();

        /// <summary>
        /// 获取所有标签的链接地址
        /// </summary>
        /// <returns></returns>
        List<Link> GetTagLinks();
    }
}