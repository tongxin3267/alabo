using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Controller {

    public abstract class ApiByIdController<TEntity, TKey> : ApiBaseController
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        #region 基础底层服务

        /// <summary>
        ///     底层基础服务
        /// </summary>

        public IService<TEntity, TKey> BaseService {
            get; set;
        }

        /// <summary>
        ///     初始化查询控制器
        /// </summary>
        protected ApiByIdController() : base() {
        }

        #endregion 基础底层服务

        #region 根据Id获取单个实体

        /// <summary>
        ///     根据ID获取视图
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet]
        [Display(Description = "根据Id获取单个实例")]
        public virtual ApiResult<TEntity> ViewById(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }
            var result = BaseService.GetViewById(id);
            return ApiResult.Success(result);
        }

        #endregion 根据Id获取单个实体

        #region 根据Id获取单个实体

        /// <summary>
        ///     根据Id获取单个实例,访问/Admin/Table/List 查看数据结构
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet]
        [Display(Description = "根据Id获取单个实例")]
        public ApiResult<TEntity> QueryById(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<TEntity>("请在控制器中定义BaseService");
            }

            if (id.IsNullOrEmpty()) {
                return ApiResult.Failure<TEntity>("Id不能为空");
            }

            var result = BaseService.GetSingle(id);
            if (result == null) {
                return ApiResult.Failure<TEntity>("数据记录不存在");
            }

            return ApiResult.Success(result);
        }

        #endregion 根据Id获取单个实体

        #region region 根据Id获取单个实例字典集合

        /// <summary>
        ///     根据Id获取单个实例字典集合
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet]
        [Display(Description = "根据Id获取单个实例字典集合")]
        public ApiResult<Dictionary<string, string>> QueryDic(string id) {
            if (BaseService == null) {
                return ApiResult.Failure<Dictionary<string, string>>("请在控制器中定义BaseService");
            }

            if (id.IsNullOrEmpty()) {
                return ApiResult.Failure<Dictionary<string, string>>("Id不能为空");
            }

            var result = BaseService.GetDictionary(id);
            if (result == null) {
                return ApiResult.Failure<Dictionary<string, string>>("数据记录不存在");
            }

            return ApiResult.Success(result);
        }

        #endregion region 根据Id获取单个实例字典集合
    }
}