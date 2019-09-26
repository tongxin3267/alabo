using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Filter;
using Alabo.Domains.Entities;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Core.WebApis.Controller {

    public abstract class ApiBaseUserController<TEntity, TKey> : ApiSingleController<TEntity, TKey>
          where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ApiBaseUserController() : base() {
        }

        #region 查询登录用户分页数据

        /// <summary>
        ///     查询登录用户分页数据，注意：只针对数据库中包含UserId的有效，支持Url参数,PageSize,PageIndex，字段名与数据库一致，
        /// </summary>
        [HttpGet]
        [Display(Description = "查询登录用户分页数据")]
        [ApiAuth]
        public ApiResult<PagedList<TEntity>> QueryUserList() {
            if (BaseService == null) {
                return ApiResult.Failure<PagedList<TEntity>>("请在控制器中定义BaseService");
            }

            var result = BaseService.GetPagedList(Query);
            return ApiResult.Success(result);
        }

        #endregion 查询登录用户分页数据
    }
}