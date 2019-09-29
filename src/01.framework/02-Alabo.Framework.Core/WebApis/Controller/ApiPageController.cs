using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Controller
{
    public abstract class ApiPageController<TEntity, TKey> : ApiOtherController<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        #region 动态Url分页查询

        /// <summary>
        ///     动态Url分页查询，支持Url参数,PageSize,PageIndex，字段名与数据库一致，
        /// </summary>
        [HttpGet]
        [Display(Description = "动态Url分页查询")]
        public ApiResult<PageResult<TEntity>> QueryPageList([FromQuery] PagedInputDto parameter)
        {
            if (BaseService == null) return ApiResult.Failure<PageResult<TEntity>>("请在控制器中定义BaseService");
            var dic = QueryDictionary();
            var result = BaseService.GetApiPagedList(dic.ToJson());
            return ApiResult.Success(result);
        }

        #endregion 动态Url分页查询
    }
}