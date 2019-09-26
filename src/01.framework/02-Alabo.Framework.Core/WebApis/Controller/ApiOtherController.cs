using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Linq;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Controller
{
    public abstract class ApiOtherController<TEntity, TKey> : ApiListController<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        #region 动态统计数据

        /// <summary>
        ///     动态统计数据
        /// </summary>
        [HttpGet]
        [Display(Description = "动态统计数据")]
        public ApiResult<IList<KeyValue>> GetKeyValue()
        {
            if (BaseService == null) return ApiResult.Failure<IList<KeyValue>>("请在控制器中定义BaseService");

            var predicate = LinqHelper.DictionaryToLinq<TEntity>(QueryDictionary());
            var expression = predicate.BuildExpression();
            var keyValues = BaseService.GetKeyValue(expression);
            return ApiResult.Success(keyValues);
        }

        #endregion 动态统计数据

        #region 动态统计数据

        /// <summary>
        ///     动态统计数据
        /// </summary>
        [HttpGet]
        [Display(Description = "动态统计数据")]
        public ApiResult<long> QueryCount()
        {
            if (BaseService == null) return ApiResult.Failure<long>("请在控制器中定义BaseService");

            var predicate = LinqHelper.DictionaryToLinq<TEntity>(QueryDictionary());
            var expression = predicate.BuildExpression();
            var count = BaseService.Count(expression);
            return ApiResult.Success(count);
        }

        #endregion 动态统计数据
    }
}