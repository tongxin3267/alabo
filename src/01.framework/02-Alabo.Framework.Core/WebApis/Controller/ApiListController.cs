using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Controller {

    public abstract class ApiListController<TEntity, TKey> : ApiDeleteController<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        #region 动态Url查询

        /// <summary>
        ///     动态Url查询，根据Url获取列表，支持Url动态参数查询，字段名与数据库一致，
        /// </summary>
        [HttpGet]
        [Display(Description = "根据Url获取列表")]
        public ApiResult<List<TEntity>> QueryList() {
            if (BaseService == null) {
                return ApiResult.Failure<List<TEntity>>("请在控制器中定义BaseService");
            }

            var result = BaseService.GetList(QueryDictionary()).ToList();
            return ApiResult.Success(result);
        }

        #endregion 动态Url查询

        [HttpGet]
        [Display(Description = "根据Url获取列表")]
        public ApiResult<List<TEntity>> QueryListAsc() {
            if (BaseService == null) {
                return ApiResult.Failure<List<TEntity>>("请在控制器中定义BaseService");
            }

            var result = BaseService.GetList(QueryDictionary()).ToList();
            result = result.OrderBy(r => r.CreateTime).ToList();
            return ApiResult.Success(result);
        }
    }
}