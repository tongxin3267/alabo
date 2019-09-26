using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.Themes.DiyModels.Links;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alabo.App.Core.Common.Domain.Services {

    public class RelationService : ServiceBase<Relation, long>, IRelationService {

        public RelationService(IUnitOfWork unitOfWork, IRepository<Relation, long> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        ///     获取所有的级联乐行
        /// </summary>
        public IEnumerable<Type> GetAllTypes() {
            var cacheKey = "Relation_GetAllTypes";
            return ObjectCache.GetOrSetPublic(() => {
                var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IRelation)) && t.FullName.EndsWith("Relation")));
                types = types.OrderBy(r => r.GetTypeInfo().GetAttribute<ClassPropertyAttribute>().SortOrder);
                return types;
            }, cacheKey).Value;
        }

        /// <summary>
        ///     检查分类下面是否存在数据
        /// </summary>
        /// <param name="script"></param>
        public bool CheckExist(string script) {
            //   return Repository.IsHavingData(script);
            return false;
        }

        /// <summary>
        ///     获取所有分类类型
        /// </summary>
        public IEnumerable<Type> GetAllClassTypes() {
            var types = GetAllTypes();
            types = types.Where(r => r.Name.Contains("Class"));
            return types;
        }

        /// <summary>
        ///     获取所有Tag类型
        /// </summary>
        public IEnumerable<Type> GetAllTagTypes() {
            var types = GetAllTypes();
            types = types.Where(r => r.Name.Contains("Tag"));
            return types;
        }

        /// <summary>
        ///     根据类型获取所有的分类
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<RelationApiOutput> GetClass(string type, long userId) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal && r.UserId == userId).OrderBy(r => r.SortOrder)
                .ToList();
            //attribute
            var relationPropert = findType.GetCustomAttribute<RelationPropertyAttribute>();
            var head = new RelationApiOutput {
                ChildClass = new List<RelationApiOutput>(),
                Id = 0
            };
            var loopList = new List<RelationApiOutput>() { head };
            while (loopList.Count > 0) {
                var first = loopList[0];
                loopList.Remove(first);
                first.ChildClass = list.Where(r => r.FatherId == first.Id).Select(child => new RelationApiOutput {
                    Name = child.Name,
                    Icon = child.Icon,
                    Id = child.Id,
                    Check = false,
                    Type = child.Type.Substring(child.Type.LastIndexOf('.') + 1),
                    UserId = child.UserId,
                    IsCanAddChild = relationPropert != null ? !relationPropert.IsOnlyRoot : true
                }).ToList();
                loopList.AddRange(first.ChildClass);
            }

            return head.ChildClass;
        }

        /// <summary>
        ///     根据类型获取所有的分类
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<RelationApiOutput> GetClass(string type) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal).OrderBy(r => r.SortOrder)
                .ToList();

            var head = new RelationApiOutput {
                ChildClass = new List<RelationApiOutput>(),
                Id = 0
            };

            var loopList = new List<RelationApiOutput>() { head };

            while (loopList.Count > 0) {
                var first = loopList[0];
                loopList.Remove(first);
                first.ChildClass = list.Where(r => r.FatherId == first.Id).Select(child => new RelationApiOutput {
                    Name = child.Name,
                    Icon = child.Icon,
                    Id = child.Id,
                    Check = false,
                    Type = child.Type.Substring(child.Type.LastIndexOf('.') + 1),
                }).ToList();
                loopList.AddRange(first.ChildClass);
            }

            return head.ChildClass;
        }

        /// <summary>
        ///     根据类型获取所有的标签
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<RelationApiOutput> GetTag(string type) {
            var findType = FindType(type);

            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal).OrderBy(r => r.SortOrder)
                .ToList();
            var resultList = new List<RelationApiOutput>();
            foreach (var item in list) {
                var keyValue = new RelationApiOutput {
                    Name = item.Name,
                    Icon = item.Icon,
                    Id = item.Id
                };
                resultList.Add(keyValue);
            }

            return resultList;
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<KeyValue> GetKeyValues(string type, long userId) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal && r.UserId == userId).OrderBy(r => r.SortOrder)
                .ToList();
            var resultList = new List<KeyValue>();
            foreach (var item in list) {
                var keyValue = new KeyValue {
                    Name = item.Name,
                    Value = item.Id
                };
                resultList.Add(keyValue);
            }

            return resultList;
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<KeyValue> GetKeyValues(string type) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal).OrderBy(r => r.SortOrder)
                .ToList();
            var resultList = new List<KeyValue>();
            foreach (var item in list) {
                var keyValue = new KeyValue {
                    Name = item.Name,
                    Value = item.Id
                };
                resultList.Add(keyValue);
            }

            return resultList;
        }

        /// <summary>
        ///     根据类型获取所有的字典类型，包括标签与分类
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<KeyValue> GetKeyValues2(string type) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal).OrderBy(r => r.SortOrder)
                .ToList();
            var resultList = new List<KeyValue>();
            foreach (var item in list) {
                var keyValue = new KeyValue {
                    Value = item.Name,
                    Key = item.Id,
                    Name = item.FatherId.ToString(),
                };
                resultList.Add(keyValue);
            }

            return resultList;
        }

        /// <summary>
        ///     根据类型获取所有的父级分类
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<RelationApiOutput> GetFatherClass(string type) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal && r.FatherId == 0)
                .OrderBy(r => r.SortOrder).ToList();
            var resultList = new List<RelationApiOutput>();
            foreach (var item in list) {
                var keyValue = new RelationApiOutput {
                    Name = item.Name,
                    Icon = item.Icon,
                    Id = item.Id
                };
                resultList.Add(keyValue);
            }

            return resultList;
        }

        /// <summary>
        ///     根据类型获取所有的父级分类，字典
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<KeyValue> GetFatherKeyValues(string type) {
            var findType = FindType(type);
            var list = GetList(r => r.Type == findType.FullName && r.Status == Status.Normal && r.FatherId == 0)
                .OrderBy(r => r.SortOrder).ToList();
            var resultList = new List<KeyValue>();
            foreach (var item in list) {
                var keyValue = new KeyValue {
                    Name = item.Name,
                    Value = item.Id
                };
                resultList.Add(keyValue);
            }

            return resultList;
        }

        /// <summary>
        ///     查找类型
        /// </summary>
        /// <param name="typeName"></param>
        public Type FindType(string typeName) {
            var findType = typeName.GetTypeByName();
            if (findType == null) {
                throw new ValidException("输入的类型不存在");
            }

            return findType;
        }

        public List<Link> GetClassLinks() {
            var list = new List<Link>();
            var result = GetAllClassTypes();

            foreach (var item in result) {
                var link = new Link();
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null) {
                    link.Name = attribute.Name;

                    link.Image = attribute.Icon;
                    var url = $"/Admin/Class?Type={item.Name}";
                    link.Url = url;
                    list.Add(link);
                }
            }

            return list;
        }

        public List<Link> GetTagLinks() {
            var list = new List<Link>();
            var result = GetAllTagTypes();

            foreach (var item in result) {
                var link = new Link();
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null) {
                    link.Name = attribute.Name;

                    link.Image = attribute.Icon;
                    var url = $"/Admin/Class?Type={item.Name}";
                    link.Url = url;
                    list.Add(link);
                }
            }

            return list;
        }
    }
}