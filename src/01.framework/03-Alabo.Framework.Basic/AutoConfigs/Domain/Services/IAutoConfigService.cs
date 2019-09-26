using System;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Core.WebUis.Models.Links;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Alabo.Framework.Basic.AutoConfigs.Domain.Services {

    public interface IAutoConfigService : IService<AutoConfig, long> {

        /// <summary>
        ///     更新配置的值
        /// </summary>
        /// <param name="config"></param>
        void AddOrUpdate(AutoConfig config);

        /// <summary>
        ///     更新配置的值
        ///     ///
        /// </summary>
        /// <param name="value"></param>
        ServiceResult AddOrUpdate<T>(object value) where T : class, IAutoConfig;

        /// <summary>
        ///     Gets all types.
        /// </summary>
        IEnumerable<Type> GetAllTypes();

        /// <summary>
        ///     根据Key获取通用配置
        /// </summary>
        /// <param name="key">完整的命名空间：Alabo.App.Core.Finance.Domain.CallBacks.WithdRawConfig</param>
        AutoConfig GetConfig(string key);

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        T GetValue<T>() where T : class, IAutoConfig;

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        List<T> GetList<T>(Func<T, bool> predicate = null) where T : new();

        IEnumerable<SelectListItem> GetList<T>(Func<T, bool> predicate, Func<T, object> textSelector,
            Func<T, object> valueSelector) where T : class, IAutoConfig;

        object GetValue(string key);

        object GetValue(Type type, Guid id);

        List<JObject> GetList(string key);

        List<object> GetObjectList(Type type);

        bool Check(string script);

        /// <summary>
        ///     根据名称获取类型
        /// </summary>
        /// <param name="name"></param>
        Type GetTypeByName(string name);

        /// <summary>
        ///     获取所有正常的货币类型
        /// </summary>
        IList<MoneyTypeConfig> MoneyTypes();

        /// <summary>
        ///     初始化所有的AutoConfig配置数据
        /// </summary>
        void InitDefaultData();

        /// <summary>
        /// 获取所有AutoConfig的链接地址
        /// </summary>
        /// <returns></returns>
        List<Link> GetAllLinks();

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="type"></param>
        void UpdateCache(string type);
    }
}