using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Convert = System.Convert;

namespace Alabo.Framework.Core.WebApis.Controller {

    public abstract class ApiDeleteController<TEntity, TKey> : ApiByIdController<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ApiDeleteController() : base() {
        }

        #region 动态删除

        /// <summary>
        ///     动态删除,删除单条记录，因为安全部分实体不支持，如需支持请在程序代码中配置
        /// </summary>
        [HttpDelete]
        [Display(Description = "删除单条记录")]
        [ApiAuth]
        public ApiResult QueryDelete([FromBody]TEntity entity) {
            var checkResult = RelationCheck(entity);
            if (!checkResult.Succeeded) {
                return ApiResult.Failure(checkResult.ToString());
            }

            var find = BaseService.GetSingle(entity.Id);
            if (find == null) {
                return ApiResult.Failure($"删除的数据不存在,Id:{entity.Id}");
            }

            var result = BaseService.Delete(entity.Id);
            if (result) {
                return ApiResult.Success("删除成功");
            }

            return ApiResult.Failure("删除失败");
        }

        #endregion 动态删除

        #region 动态删除

        /// <summary>
        ///     动态删除,删除单条记录，因为安全部分实体不支持，如需支持请在程序代码中配置
        /// </summary>
        [HttpDelete]
        [Display(Description = "删除单条记录")]
        [ApiAuth]
        public ApiResult QueryUserDelete([FromBody]TEntity entity) {
            var checkResult = RelationCheck(entity);
            if (!checkResult.Succeeded) {
                return ApiResult.Failure(checkResult.ToString());
            }
            var find = BaseService.GetSingle(entity.Id);
            if (find == null) {
                return ApiResult.Failure($"删除的数据不存在,Id:{entity.Id}");
            }

            var dynamicFind = (dynamic)find;
            if (Convert.ToInt64(dynamicFind.UserId) != AutoModel.BasicUser && AutoModel.BasicUser.Id > 0) {
                return ApiResult.Failure("您无权删除该数据");
            }

            var result = BaseService.Delete(entity.Id);
            if (result) {
                return ApiResult.Success("删除成功");
            }

            return ApiResult.Failure("删除失败");
        }

        #endregion 动态删除

        #region 批量删除

        /// <summary>
        ///     批量删除,因为安全部分实体不支持，注意：body参数需要添加引号，"'1,2,3'"而不是"1,2,3"
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        [HttpPost]
        [Display(Description = "删除单条记录")]
        [ApiAuth]
        public ApiResult BatchDelete([FromBody] string ids) {
            return ApiResult.Failure("改类型不支持api 接口删除");
        }

        #endregion 批量删除

        /// <summary>
        /// 删除前判断级联数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ServiceResult RelationCheck(TEntity entity) {
            if (entity.Id.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage($"{typeof(TEntity).Name}Id不能为空");
            }
            var deleteAttribute = typeof(TEntity).FullName.GetAutoDeleteAttribute();
            if (deleteAttribute == null) {
                return ServiceResult.FailedWithMessage($"{typeof(TEntity).Name}未配置删除特性，不能删除");
            }
            if (deleteAttribute.IsAuto && deleteAttribute.EntityType == null) {
                return ServiceResult.Success;
            }
            if (deleteAttribute.EntityType == null) {
                return ServiceResult.FailedWithMessage($"{typeof(TEntity).Name}未配置删除级联，不能删除");
            }

            if (deleteAttribute.RelationId.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage($"{typeof(TEntity).Name}未配置RelationId，不能删除");
            }
            var entityRelation =
                // var lambda= Lambda.Equal<>()
                Alabo.Linq.Dynamic.DynamicService.ResolveMethod(deleteAttribute.EntityType.Name, "GetSingle", deleteAttribute.RelationId,
                    entity.Id);
            if (!entityRelation.Item1.Succeeded) {
                return ServiceResult.FailedWithMessage(entityRelation.Item1.ToString());
            }

            if (entityRelation.Item2 != null) {
                return ServiceResult.FailedWithMessage($"有关联数据不能删除");
            }

            return ServiceResult.Success;
        }
    }
}