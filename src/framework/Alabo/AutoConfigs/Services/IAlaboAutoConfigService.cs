using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IAlaboAutoConfigService : IService<AutoConfig, long> {

        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetValue<T>() where T : class, IAutoConfig;

        /// <summary>
        ///     获取多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        List<T> GetList<T>(Func<T, bool> predicate = null) where T : new();

        List<object> GetObjectList(Type type);

        /// <summary>
        ///     根据Key获取通用配置
        /// </summary>
        /// <param name="key">完整的命名空间：Alabo.App.Core.Finance.Domain.CallBacks.WithdRawConfig</param>
        AutoConfig GetConfig(string key);
    }
}