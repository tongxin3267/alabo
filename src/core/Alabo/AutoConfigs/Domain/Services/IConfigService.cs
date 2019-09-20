using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IConfigService : IService<AutoConfig, long> {

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
    }
}