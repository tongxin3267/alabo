using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Api.Controller {

    public abstract class ApiSingleController<TEntity, TKey> : ApiSaveController<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ApiSingleController() : base() {
        }

        #region 根据Id和字段名称,获取单个字段的值

        /// <summary>
        ///     根据Id和字段名称,获取单个字段的值
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="field">字段</param>
        [Display(Description = " 根据Id和字段名称,获取单个字段的值")]
        [HttpGet]
        public ApiResult QueryFieldValue(string id, string field) {
            if (field.IsNullOrEmpty()) {
                return ApiResult.Failure("Field不能为空");
            }

            if (id.ToStr().IsNullOrEmpty()) {
                return ApiResult.Failure("Id不能为空");
            }

            var value = BaseService.GetFieldValue(id, field);
            return ApiResult.Success(value);
        }

        #endregion 根据Id和字段名称,获取单个字段的值

        #region 根据Id获取下一条记录

        /// <summary>
        ///     默认数据
        /// </summary>
        [HttpGet]
        [Display(Description = "根据Id获取下一条记录")]
        public ApiResult<TEntity> QueryDefault() {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }
            var result = BaseService.FirstOrDefault();
            if (result == null) {
                return ApiResult.Failure<TEntity>("数据记录不存在");
            }

            return ApiResult.Success(result);
        }

        #endregion 根据Id获取下一条记录

        #region 根据Id获取下一条记录

        /// <summary>
        ///     根据Id获取下一条记录
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet]
        [Display(Description = "根据Id获取下一条记录")]
        public ApiResult<TEntity> NextById(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }

            if (id.IsNullOrEmpty()) {
                return ApiResult.Failure<TEntity>("Id不能为空");
            }

            var result = BaseService.Next(id);
            if (result == null) {
                return ApiResult.Failure<TEntity>("数据记录不存在");
            }

            return ApiResult.Success(result);
        }

        #endregion 根据Id获取下一条记录

        #region 根据Id获取上一条记录

        /// <summary>
        ///     根据Id获取上一条记录
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet]
        [Display(Description = "根据Id获取上一条记录")]
        public ApiResult<TEntity> PrexById(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }

            if (id.IsNullOrEmpty()) {
                return ApiResult.Failure<TEntity>("Id不能为空");
            }

            var result = BaseService.Prex(id);
            if (result == null) {
                return ApiResult.Failure<TEntity>("数据记录不存在");
            }

            return ApiResult.Success(result);
        }

        #endregion 根据Id获取上一条记录
    }
}