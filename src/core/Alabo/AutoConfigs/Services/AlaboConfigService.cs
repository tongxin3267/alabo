using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alabo.App.Core.Common.Domain.Services {

    public class AlaboConfigService : ServiceBase<AutoConfig, long>, IAlaboConfigService {
        private static readonly string AutoConfigCacheKey = "AutoConfigCacheKey_";

        public AlaboConfigService(IUnitOfWork unitOfWork, IRepository<AutoConfig, long> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        ///     获取配置信息
        /// </summary>
        /// <param name="key"></param>
        public AutoConfig GetConfig(string key) {
            if (string.IsNullOrWhiteSpace(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            var cacheKey = AutoConfigCacheKey + key;
            if (!ObjectCache.TryGet(cacheKey, out AutoConfig config)) {
                config = Repository<IAutoConfigRepository>().GetSingle(e => e.Type == key);
                if (config != null) {
                    ObjectCache.Set(cacheKey, config);
                }
            }

            return config;
        }

        public T GetValue<T>() where T : class, IAutoConfig {
            var config = GetConfig(typeof(T).FullName);
            if (config == null) {
                return Activator.CreateInstance(typeof(T)) as T;
            }

            var result = JsonConvert.DeserializeObject<T>(config.Value);
            return result;
        }

        public object GetValue(string key) {
            var types = GetAllTypes();
            foreach (var item in types) {
                if (item.FullName == key) {
                    return GetValue(item);
                }
            }

            return null;
        }

        private object GetValue(Type type) {
            var config = GetConfig(type.FullName);
            if (config != null) {
                var configDescription = new ClassDescription(config.GetType());
                //如果是编辑页面获取数据库里头的字，如果列表页面使用 GetList<T>来获取值
                //如果列表页面编辑的时候，应该要传入ID
                if (configDescription.ClassPropertyAttribute.PageType == ViewPageType.Edit) {
                    var data = Activator.CreateInstance(type);
                    var request = JsonConvert.DeserializeObject<JObject>(config.Value);
                    PropertyDescription.SetValue(data, request);
                    return data;
                }

                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(type);
        }

        public IEnumerable<Type> GetAllTypes() {
            var cacheKey = AutoConfigCacheKey + "_alltypes";
            if (!ObjectCache.TryGetPublic(cacheKey, out IEnumerable<Type> types)) {
                //因为遍历所有程序集，速度会有影响
                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IAutoConfig))));
                //排序，根据SordOrder从小到大排列
                types = types.OrderBy(r =>
                    r.GetTypeInfo().GetAttribute<ClassPropertyAttribute>() != null
                        ? r.GetTypeInfo().GetAttribute<ClassPropertyAttribute>().SortOrder
                        : 1);
                ObjectCache.Set(cacheKey, types);
            }

            return types;
        }

        public List<T> GetList<T>(Func<T, bool> predicate = null) where T : new() {
            var config = GetConfig(typeof(T).FullName);
            var t = new T();
            var configlist = new List<T>();
            if (config != null) {
                if (config.Value != null) {
                    configlist = config.Value.Deserialize(t);
                    if (predicate != null) {
                        return configlist.Where(predicate).ToList();
                    }
                }
            }

            return configlist;
        }

        /// <summary>
        ///     获取值的类型
        /// </summary>
        /// <param name="type">值类型</param>
        /// <param name="id">如果是列表页面需要传入ID,编辑页面不需要传入</param>
        public object GetValue(Type type, Guid id) {
            var config = GetConfig(type.FullName);
            var data = Activator.CreateInstance(type);

            // 如果包含Id的字段
            var idField = type.GetProperty("Id");
            if (idField != null) {
                if (id.IsGuidNullOrEmpty()) {
                    return data;
                }
            }

            if (config != null) {
                var configDescription = new ClassDescription(data.GetType());
                var classDescription = new ClassDescription(type);
                //获取  Json有扩展的属性
                var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty())
                    .ToList();

                if (configDescription.ClassPropertyAttribute.PageType == ViewPageType.List) {
                    var request = JsonConvert.DeserializeObject<List<JObject>>(config.Value);
                    foreach (var item in request) {
                        PropertyDescription.SetValue(data, item);
                        if (data.GetType().GetProperty("Id").GetValue(data).ToString() == id.ToString()) {
                            if (propertys.Any()) {
                                // json 格式数据处理
                                data = item.ToObject(type);
                                return data;
                            } else {
                                return data;
                            }
                        }
                    }
                } else {
                    var request = JsonConvert.DeserializeObject<JObject>(config.Value);
                    PropertyDescription.SetValue(data, request);
                    //  data = JsonMapping.HttpContextToExtension(data, type, HttpContext);
                    return data;
                }

                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(type);
        }
    }
}