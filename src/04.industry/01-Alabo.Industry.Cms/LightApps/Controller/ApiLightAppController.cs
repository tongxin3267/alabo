using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.LightApps.Domain;
using Alabo.App.Core.LightApps.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Helpers;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.LightApps.Controller {

    [ApiExceptionFilter]
    [Route("Api/LightApp/[action]")]
    public class ApiLightAppController : ApiBaseController {

        public ApiLightAppController() : base() {
        }

        [HttpPost]
        public ApiResult Add([FromBody] LightAppAddView input) {
            if (input.TableName.IsNullOrEmpty()) {
                return ApiResult.Failure("未正确指定数据表表名");
            }

            var rs = Ioc.Resolve<ILightAppService>().Add(input.TableName, input.DataJson);

            return ApiResult.Success("添加成功!");
        }

        [HttpPost]
        public ApiResult Update([FromBody] LightAppAddView input) {
            if (input.TableName.IsNullOrEmpty()) {
                return ApiResult.Failure("未正确指定数据表表名");
            }

            var rs = Ioc.Resolve<ILightAppService>().Update(input.TableName, input.DataJson, input.Id.ToObjectId());

            return ApiResult.Success("更新成功!");
        }

        [HttpDelete]
        public ApiResult Delete(string tableName, string id) {
            if (!ObjectId.TryParse(id, out ObjectId oId)) {
                return ApiResult.Failure("参数id不是有效的ObjectId!");
            }

            var rs = Ioc.Resolve<ILightAppService>().Delete(tableName, oId);

            return rs.Succeeded ? ApiResult.Success("删除成功!") : ApiResult.Failure("删除失败!");
        }

        [HttpGet]
        public ApiResult GetSingle(string tableName, string id) {
            var dic = new Dictionary<string, string>
            {
                {"_id", id },
            };
            var model = Ioc.Resolve<ILightAppService>().GetSingle(tableName, dic);
            if (model != null) {
                return ApiResult.Success(model);
            }

            return ApiResult.Failure("未查到相应数据");
        }

        /// <summary>
        ///     动态Url查询，根据Url获取列表，支持Url动态参数查询，字段名与数据库一致，
        /// </summary>
        [HttpGet]
        [Display(Description = "根据Url获取列表")]
        public ApiResult<List<dynamic>> GetList() {
            var tableName = "";
            var dicQuery = QueryDictionary();
            if (dicQuery.ContainsKey("TableName")) {
                tableName = dicQuery["TableName"];
                dicQuery.Remove("TableName");
            } else {
                return ApiResult.Failure<List<dynamic>>("参数错误: 未指定对应的数据表名!");
            }

            var result = Ioc.Resolve<ILightAppService>().GetList(tableName, dicQuery).ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///    分页获取List
        /// </summary>
        [HttpGet]
        [Display(Description = "根据Url获取列表")]
        public ApiResult<PageResult<dynamic>> GetPagedList() {
            var tableName = "";
            var dicQuery = QueryDictionary();
            if (dicQuery.ContainsKey("TableName")) {
                tableName = dicQuery["TableName"];
                dicQuery.Remove("TableName");
            } else {
                return ApiResult.Failure<PageResult<dynamic>>("参数错误: 未指定对应的数据表名!");
            }

            var result = Ioc.Resolve<ILightAppService>().GetPagedList(tableName, dicQuery.ToJsons());
            var apiRusult = new PageResult<dynamic> {
                PageCount = result.PageCount,
                Result = result,
                RecordCount = result.RecordCount,
                CurrentSize = result.CurrentSize,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };

            return ApiResult.Success(apiRusult);
        }
    }
}