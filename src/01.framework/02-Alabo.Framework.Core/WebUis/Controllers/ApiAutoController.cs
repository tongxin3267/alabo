using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.UI.Domain.Services;
using Alabo.App.Core.UI.Dtos;
using Alabo.Domains.Entities.Core;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoArticles;
using Alabo.UI.AutoForms;
using Alabo.UI.AutoLists;
using Alabo.UI.AutoNews;
using Alabo.UI.AutoPreviews;
using Alabo.UI.AutoReports;
using Alabo.UI.AutoTables;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Services.Report;
using ZKCloud.Open.ApiBase.Models;
using IAutoPreview = Alabo.UI.AutoPreviews.IAutoPreview;

namespace Alabo.App.Core.UI.Controllers {

    /// <summary>
    /// 对应前端 form  image table task report等通用页面
    /// </summary>
    [Route("Api/Auto/[action]")]
    public class ApiAutoController : ApiBaseController {

        /// <summary>
        /// constructor
        /// </summary>
        public ApiAutoController() : base() {
        }

        #region AutoForm相关操作

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        public ApiResult<AutoForm> Form([FromQuery] string type, string id) {
            // Url查询参数
            AutoModel.Query = Query;
            var result = Resolve<IAutoFormServcie>().GetView(type, id, AutoModel);
            return ToResult(result);
        }

        /// <summary>
        /// AutoForm保存
        /// </summary>
        [HttpPost]
        public ApiResult Save([FromBody] AutoFormInput autoFormInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }
            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(autoFormInput.Type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return ApiResult.Failure(checkType.ToString());
            }
            autoFormInput.TypeFind = typeFind;
            autoFormInput.TypeInstance = instanceFind;
            try {
                autoFormInput.ModelFind = autoFormInput.Model.ToObject(typeFind);
            } catch {
                return ApiResult.Failure("序列化出错");
            }
            // 表单数据验证,特性验证
            var model = autoFormInput.ModelFind;
            if (model == null) {
                return ApiResult.Failure("保存数据出错");
            }
            if (!IsFormValid(ref model, out var error)) {
                return ApiResult.Failure(error);
            }

            var result = Resolve<IAutoFormServcie>().Save(autoFormInput, AutoModel);
            return ToResult(result);
        }

        #endregion AutoForm相关操作

        #region 导出Excel

        /// <summary>
        /// 导出Excel
        /// </summary>
        [HttpGet]
        [Display(Description = "导出Excel")]
        public ApiResult ToExcel([FromQuery] string type) {
            var dataResult = Resolve<IAutoTableService>().Table(type, QueryDictionary().ToJsons(), AutoModel);
            if (!dataResult.Item1.Succeeded) {
                return ApiResult.Failure(dataResult.Item1.ErrorMessages.ToString());
            }
            var modelType = type.GetTypeByName();
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure(new { File = "" }, $"失败: 类型不能为空");
            }

            var autoTable = dataResult.Item2;

            try {
                var result = Resolve<IAdminTableService>().ToExcel(modelType, autoTable.Result);

                var index = result.Item2.LastIndexOf(@"\wwwroot\");
                if (index < 0) {
                    return ApiResult.Failure(new { File = "" }, $"失败: 文件生成失败");
                }
                var fileBytes = System.IO.File.ReadAllBytes(result.Item2);
                var file = File(fileBytes, "application/x-msdownload", result.Item3);

                var path = $"{result.Item2.Substring(index)}".Replace(@"\", "/");
                return ApiResult.Success(path);
            } catch (Exception ex) {
                return ApiResult.Failure(new { File = "" }, ex.Message);
            }
        }

        #endregion 导出Excel

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        [ApiAuth(Filter = FilterType.Admin)]
        public ApiResult<AutoTable> Table([FromQuery] string type) {
            var tenant = HttpContext.Request.Headers["zk-tenant"];
            if (AutoModel != null && AutoModel.BasicUser != null) {
                AutoModel.BasicUser.Tenant = tenant;
            }
            var result = Resolve<IAutoTableService>().Table(type, QueryDictionary().ToJsons(), AutoModel);
            return ToResult(result);
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        public ApiResult<AutoList> List([FromQuery] string type) {
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure<AutoList>("类型不能为空");
            }

            var modelType = type.GetTypeByName();
            var find = type.GetInstanceByName();
            if (modelType == null || find == null) {
                return ApiResult.Failure<AutoList>($"类型不存在，请确认{type}输入是否正确");
            }

            var autoList = new AutoList();
            if (find is IAutoList set) {
                var searchType = set.SearchType();
                autoList = AutoListMapping.Convert(searchType.FullName);
                autoList.Result = set.PageList(this.Query, AutoModel);
            }
            return ApiResult.Success(autoList);
        }

        /// <summary>
        /// Get preview of object id.
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        public ApiResult<AutoPreview> Preview(string id, string type) {
            //check
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure<AutoPreview>("类型不能为空");
            }

            if (id.IsNullOrEmpty()) {
                return ApiResult.Failure<AutoPreview>("id不能为空");
            }

            var typeIntance = type.GetTypeByName();
            var find = type.GetInstanceByName();
            if (find == null || typeIntance == null) {
                return ApiResult.Failure<AutoPreview>($"类型不存在，请确认{type}输入是否正确");
            }

            //entity
            if (find is IEntity) {
                var obj = Linq.Dynamic.DynamicService.ResolveMethod(typeIntance.Name, "GetViewById", id);
                var autoPreview = AutoPreviewMapping.Convert(obj);
                return ApiResult.Success(autoPreview);
            }

            //Auto preview
            if (find is IAutoPreview set) {
                var autoPreview = set.GetPreview(id, AutoModel);
                return ApiResult.Success(autoPreview);
            }

            return ApiResult.Success(AutoPreviewMapping.Convert(find.GetType().FullName));
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取新闻列表")]
        public ApiResult<AutoNews> News([FromQuery] string type) {
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure<AutoNews>("类型不能为空");
            }

            var find = type.GetInstanceByName();

            if (find is IAutoNews set) {
                //约定不完善
                //if (!type.EndsWith("News") || !type.Contains("AutoNews")){
                //if (!type.EndsWith("Image") || !type.Contains("AutoImage")) {
                //    if (!type.EndsWith("Video") || !type.Contains("AutoVideo")) {
                //        if (!type.EndsWith("Faq") || !type.Contains("AutoFaq")) {
                //            if (!type.EndsWith("Recruit") || !type.Contains("AutoRecruit")) {
                //                return ApiResult.Failure<AutoNews>($"类型{type}的命名约定，不符合约定");
                //            }
                //        }
                //    }
                // }
                //}
                var autoNews = new AutoNews {
                    ResultList = set.ResultList(Query, AutoModel),
                    Setting = set.Setting()
                };
                return ApiResult.Success(autoNews);
            } else {
                return ApiResult.Failure<AutoNews>($"非IAutoNews类型，请确认{type}输入是否正确");
            }
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取文章")]
        public ApiResult<AutoArticle> Article([FromQuery] string type, string id) {
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure<AutoArticle>("类型不能为空");
            }

            var find = type.GetInstanceByName();
            if (find == null) {
                return ApiResult.Failure<AutoArticle>($"类型不存在，请确认{type}输入是否正确");
            }

            if (find is IAutoArticle set) {
                if (!type.EndsWith("Article") || !type.Contains("AutoArticle")) {
                    return ApiResult.Failure<AutoArticle>($"类型{type}的命名约定，不符合约定");
                }

                var auto = new AutoArticle {
                    ResultList = set.ResultList(id),
                    Setting = set.Setting()
                };
                return ApiResult.Success(auto);
            } else {
                return ApiResult.Failure<AutoArticle>($"非IAutoNews类型，请确认{type}输入是否正确");
            }
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取文章")]
        public ApiResult<List<AutoReport>> Report([FromQuery] string type) {
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure<List<AutoReport>>("类型不能为空");
            }

            var find = type.GetInstanceByName();
            if (find == null) {
                return ApiResult.Failure<List<AutoReport>>($"类型不存在，请确认{type}输入是否正确");
            }

            if (find is IAutoReport set) {
                if (!type.EndsWith("Report")) {
                    return ApiResult.Failure<List<AutoReport>>($"类型{type}的命名约定，不符合约定");
                }

                var result = set.ResultList(Query, AutoModel);
                return ApiResult.Success(result);
            } else {
                return ApiResult.Failure<List<AutoReport>>($"非IAutoReport类型，请确认{type}输入是否正确");
            }
        }
    }
}