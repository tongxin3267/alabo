using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Mapping;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Controller {

    public abstract class ApiSaveController<TEntity, TKey> : ApiPageController<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ApiSaveController() : base() {
        }

        #region 增加单条记录

        /// <summary>
        ///     修改或添加
        /// </summary>
        [HttpPost]
        [Display(Description = "修改或删除")]
        public ApiResult<TEntity> QuerySave([FromBody]TEntity paramaer) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }

            if (paramaer == null) {
                return ApiResult.Failure<TEntity>("参数值为空,请认真检查服务器特性配置：比如ObjectId是否配置特性");
            }
            if (!this.IsFormValid()) {
                return ApiResult.Failure<TEntity>(this.FormInvalidReason());
            }

            var find = BaseService.GetSingle(paramaer.Id);
            if (find == null) {
                var result = BaseService.Add(paramaer);
                if (result == false) {
                    return ApiResult.Failure<TEntity>("添加失败");
                }
                return ApiResult.Success(paramaer);
            } else {
                find = AutoMapping.SetValue<TEntity>(paramaer);
                var result = BaseService.Update(find);
                if (result == false) {
                    return ApiResult.Failure<TEntity>("添加失败");
                }

                return ApiResult.Success(find);
            }
        }

        #endregion 增加单条记录

        #region 增加单条记录

        /// <summary>
        ///     增加单条记录，因为安全部分实体不支持，如需支持请在程序代码中配置
        /// </summary>
        [HttpPost]
        [Display(Description = "增加单条记录")]
        public ApiResult<TEntity> QueryAdd([FromBody]TEntity paramaer) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }
            if (!this.IsFormValid()) {
                return ApiResult.Failure<TEntity>(this.FormInvalidReason());
            }

            var result = BaseService.Add(paramaer);
            if (result == false) {
                return ApiResult.Failure<TEntity>("添加失败");
            }

            return ApiResult.Success(paramaer);
        }

        #endregion 增加单条记录

        #region 修改单条记录

        /// <summary>
        ///     修改单条记录，因为安全部分实体不支持，如需支持请在程序代码中配置
        /// </summary>
        [HttpPut]
        [Display(Description = "修改单条记录")]
        [HttpPost]
        public ApiResult<TEntity> QueryUpdate([FromBody]TEntity paramaer) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }

            if (!this.IsFormValid()) {
                return ApiResult.Failure<TEntity>(this.FormInvalidReason());
            }

            var find = BaseService.GetSingle(paramaer.Id);
            if (find == null) {
                return ApiResult.Failure<TEntity>("请输入正确的Id");
            }
            //var dynamicFind = (dynamic)find;
            //if (Convert.ToInt64(dynamicFind.UserId) != LoginUserId && LoginUserId > 0) {
            //    return ApiResult.Failure<TEntity>("您无权删除该数据");
            //}

            var result = BaseService.Update(find);
            if (result == false) {
                return ApiResult.Failure<TEntity>("更新失败");
            }

            return ApiResult.Success(find);
        }

        #endregion 修改单条记录
    }
}