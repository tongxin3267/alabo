using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.AutoConfigs.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alabo.Framework.Basic.AutoConfigs.Domain.Services
{
    public class AutoConfigService : ServiceBase<AutoConfig, long>, IAutoConfigService
    {
        private static readonly string AutoConfigCacheKey = "AutoConfigCacheKey_";

        public AutoConfigService(IUnitOfWork unitOfWork, IRepository<AutoConfig, long> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     更新配置的值
        /// </summary>
        /// <param name="value"></param>
        public ServiceResult AddOrUpdate<T>(object value) where T : class, IAutoConfig
        {
            var autoConfig = Resolve<IAutoConfigService>().GetConfig(typeof(T).FullName);
            var typeclassProperty = typeof(T).GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
            if (typeclassProperty == null) return ServiceResult.FailedWithMessage("未设置ClassPropertyAttribute特性");

            if (autoConfig == null)
                autoConfig = new AutoConfig
                {
                    AppName = Resolve<ITypeService>().GetAppName(typeof(T)),
                    Type = typeof(T).FullName
                };

            autoConfig.LastUpdated = DateTime.Now;
            autoConfig.Value = value.ToJson();
            AddOrUpdate(autoConfig);
            return ServiceResult.Success;
        }

        public void AddOrUpdate(AutoConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            AutoConfig find = null;
            if (config.Id > 0) find = Repository<IAutoConfigRepository>().GetSingle(e => e.Id == config.Id);

            if (find == null) find = Repository<IAutoConfigRepository>().GetSingle(e => e.Type == config.Type);
            var appName = Resolve<ITypeService>().GetAppName(config.Type);
            if (find == null)
            {
                find = new AutoConfig
                {
                    AppName = appName,
                    Type = config.Type,
                    Value = config.Value,
                    LastUpdated = DateTime.Now
                };
                Add(find);
            }
            else
            {
                find.AppName = appName;
                find.Type = config.Type;
                find.Value = config.Value;
                find.LastUpdated = DateTime.Now;
                Update(find);
            }

            UpdateCache(config.Type); // 更新缓存
        }

        /// <summary>
        ///     获取配置信息
        /// </summary>
        /// <param name="key"></param>
        public AutoConfig GetConfig(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var cacheKey = AutoConfigCacheKey + key;
            if (!ObjectCache.TryGet(cacheKey, out AutoConfig config))
            {
                config = Repository<IAutoConfigRepository>().GetSingle(e => e.Type == key);
                if (config != null) ObjectCache.Set(cacheKey, config);
            }

            return config;
        }

        public T GetValue<T>() where T : class, IAutoConfig
        {
            var config = GetConfig(typeof(T).FullName);
            if (config == null) return Activator.CreateInstance(typeof(T)) as T;

            var result = JsonConvert.DeserializeObject<T>(config.Value);
            return result;
        }

        public object GetValue(string key)
        {
            var types = GetAllTypes();
            foreach (var item in types)
                if (item.FullName == key)
                    return GetValue(item);

            return null;
        }

        public IEnumerable<Type> GetAllTypes()
        {
            var cacheKey = AutoConfigCacheKey + "_alltypes";
            if (!ObjectCache.TryGetPublic(cacheKey, out IEnumerable<Type> types))
            {
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

        /// <summary>
        ///     根据命名空间获取类型
        /// </summary>
        /// <param name="name"></param>
        public Type GetTypeByName(string name)
        {
            var types = GetAllTypes();
            foreach (var item in types)
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                    item.FullName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return item;

            return null;
        }

        public List<JObject> GetList(string key)
        {
            var list = new List<JObject>();
            var dataConfig = GetConfig(key);
            if (dataConfig != null) list = JsonConvert.DeserializeObject<List<JObject>>(dataConfig.Value);

            return list;
        }

        public List<T> GetList<T>(Func<T, bool> predicate = null) where T : new()
        {
            var config = GetConfig(typeof(T).FullName);
            var t = new T();
            var configlist = new List<T>();
            if (config != null)
                if (config.Value != null)
                {
                    configlist = config.Value.Deserialize(t);
                    if (predicate != null) return configlist.Where(predicate).ToList();
                }

            return configlist;
        }

        public IEnumerable<SelectListItem> GetList<T>(Func<T, bool> predicate, Func<T, object> textSelector,
            Func<T, object> valueSelector) where T : class, IAutoConfig
        {
            var config = GetConfig(typeof(T).FullName);
            var values = new List<T>();
            if (config != null)
            {
                var request = JsonConvert.DeserializeObject<List<JObject>>(config.Value);
                foreach (var item in request)
                {
                    var data = (T) Activator.CreateInstance(typeof(T));
                    PropertyDescription.SetValue(data, item);
                    values.Add(data);
                }
            }
            else
            {
                return null;
            }

            return FromIEnumerable(values.Where(predicate).ToList(), textSelector, valueSelector);
        }

        /// <summary>
        ///     获取值的类型
        /// </summary>
        /// <param name="type">值类型</param>
        /// <param name="id">如果是列表页面需要传入ID,编辑页面不需要传入</param>
        public object GetValue(Type type, Guid id)
        {
            var config = GetConfig(type.FullName);
            var data = Activator.CreateInstance(type);

            // 如果包含Id的字段
            var idField = type.GetProperty("Id");
            if (idField != null)
                if (id.IsGuidNullOrEmpty())
                    return data;

            if (config != null)
            {
                var configDescription = new ClassDescription(data.GetType());
                var classDescription = new ClassDescription(type);
                //获取  Json有扩展的属性
                var propertys = classDescription.Propertys.Where(r => !r.FieldAttribute.ExtensionJson.IsNullOrEmpty())
                    .ToList();

                if (configDescription.ClassPropertyAttribute.PageType == ViewPageType.List)
                {
                    var request = JsonConvert.DeserializeObject<List<JObject>>(config.Value);
                    foreach (var item in request)
                    {
                        PropertyDescription.SetValue(data, item);
                        if (data.GetType().GetProperty("Id").GetValue(data).ToString() == id.ToString())
                        {
                            if (propertys.Any())
                            {
                                // json 格式数据处理
                                data = item.ToObject(type);
                                return data;
                            }

                            return data;
                        }
                    }
                }
                else
                {
                    var request = JsonConvert.DeserializeObject<JObject>(config.Value);
                    PropertyDescription.SetValue(data, request);
                    //  data = JsonMapping.HttpContextToExtension(data, type, HttpContext);
                    return data;
                }

                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(type);
        }

        public List<object> GetObjectList(Type type)
        {
            var list = new List<object>();
            var config = GetConfig(type.FullName);
            if (config != null)
            {
                var request = JsonConvert.DeserializeObject<List<JObject>>(config.Value);
                foreach (var item in request)
                {
                    var data = Activator.CreateInstance(type);
                    PropertyDescription.SetValue(data, item);
                    list.Add(data);
                }
            }

            return list;
        }

        public bool Check(string script)
        {
            // return Repository.IsHavingData(script);
            return false;
        }

        /// <summary>
        ///     获取所有正常的货币类型
        /// </summary>
        public IList<MoneyTypeConfig> MoneyTypes()
        {
            return GetList<MoneyTypeConfig>(r => r.Status == Status.Normal);
        }

        /// <summary>
        ///     初始化所有的AutoConfig配置数据
        /// </summary>
        public void InitDefaultData()
        {
            //Delete(r => r.Type == typeof(PostRoleConfig).FullName); // 临时删除所有权限
            foreach (var type in GetAllTypes())
                try
                {
                    var config = Activator.CreateInstance(type);
                    if (config is IAutoConfig set) set.SetDefault();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
        }

        /// <summary>
        ///     Updates the cache.
        /// </summary>
        /// <param name="type">The type.</param>
        public void UpdateCache(string type)
        {
            var cacheKey = AutoConfigCacheKey + type;
            ObjectCache.Remove(cacheKey);
            // 如果是修改货币类型，或修改商城模式，可能会导致商品的价格发生变化
            // 通过缓存开启价格更新设置 Alabo.App.Shop.Product.Schedules.ProductPriceSchedule 将在1分钟内完成价格更新
            if (type == "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig" ||
                type == "Alabo.App.Shop.Product.Domain.CallBacks.PriceStyleConfig")
            {
                var productPriceScheduleKey = "ProductPriceSchedule";
                ObjectCache.Set(productPriceScheduleKey, true);
            }

            //// 修改二维码配置时，会在1分钟内自动更新所有会员的二维码
            //if (type == typeof(QrCodeConfig).FullName) {
            //    Resolve<IUserQrCodeService>().CreateCodeTask();
            //}

            // 修改二维码配置时，会在1分钟内自动更新所有会员的二维码
            //if (type == "Alabo.App.Core.User.Domain.CallBacks.UserConfig") {
            //    var userMapInfoUpdateSchedule = "UserMapInfoUpdateSchedule";
            //    var userConfig = GetValue<TeamConfig>();
            //    if (ObjectCache.TryGet(userMapInfoUpdateSchedule, out Tuple<bool, long> result)) {
            //        // 团队数量更新
            //        if (result.Item2 != userConfig.TeamLevel) {
            //            Resolve<IGradeInfoService>().UpdataAllUserBackJob();
            //            ObjectCache.Set(userMapInfoUpdateSchedule, Tuple.Create(true, userConfig.TeamLevel));
            //        }
            //    } else {
            //        ObjectCache.Set(userMapInfoUpdateSchedule, Tuple.Create(true, userConfig.TeamLevel));
            //    }
            //}

            var cacheAllKey = AutoConfigCacheKey + "_alltypes";
            ObjectCache.Remove(cacheAllKey);
        }

        public List<Link> GetAllLinks()
        {
            var list = new List<Link>();
            var result = Resolve<IAutoConfigService>().GetAllTypes();

            foreach (var item in result)
            {
                var link = new Link();
                var attribute = item.GetAttribute<ClassPropertyAttribute>();
                if (attribute != null)
                {
                    link.Name = attribute.Name;

                    link.Image = attribute.Icon;
                    if (attribute.PageType == ViewPageType.Edit)
                    {
                        var url = $"Admin/AutoConfig/Edit?key={item.FullName}";
                        link.Url = url;
                    }

                    if (attribute.PageType == ViewPageType.List)
                    {
                        var url = $"Admin/AutoConfig/Edit?List={item.FullName}";
                        link.Url = url;
                    }

                    list.Add(link);
                }
            }

            return list;
        }

        private object GetValue(Type type)
        {
            var config = GetConfig(type.FullName);
            if (config != null)
            {
                var configDescription = new ClassDescription(config.GetType());
                //如果是编辑页面获取数据库里头的字，如果列表页面使用 GetList<T>来获取值
                //如果列表页面编辑的时候，应该要传入ID
                if (configDescription.ClassPropertyAttribute.PageType == ViewPageType.Edit)
                {
                    var data = Activator.CreateInstance(type);
                    var request = JsonConvert.DeserializeObject<JObject>(config.Value);
                    PropertyDescription.SetValue(data, request);
                    return data;
                }

                return Activator.CreateInstance(type);
            }

            return Activator.CreateInstance(type);
        }

        public Type GetType(string key)
        {
            var types = GetAllTypes();
            foreach (var item in types)
                if (item.FullName == key)
                    return item;

            return null;
        }

        public static IEnumerable<SelectListItem> FromIEnumerable<T>(
            IEnumerable<T> elements,
            Func<T, object> textSelector, Func<T, object> valueSelector)
        {
            foreach (var element in elements)
                yield return new SelectListItem
                {
                    Text = textSelector(element)?.ToString(),
                    Value = valueSelector(element)?.ToString()
                };
        }
    }
}