﻿using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebUis.Services;
using Alabo.Tables.Domain.Services;
using Alabo.UI.Design.AutoTables;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.AutoConfigs;
using Alabo.Dynamics;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Framework.Core.Reflections.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebUis.Controllers
{
    /// <summary>
    ///     Api type controller
    /// </summary>
    [Route("Api/Type/[action]")]
    public class ApiTypeController : ApiBaseController
    {
        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        public ApiResult<AutoTable> FormNoData([FromQuery] string type) {
            var result = Resolve<IAutoTableService>().TableNoData(type, QueryDictionary().ToJsons(), AutoModel);
            return ToResult(result);
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        public ApiResult<AutoTable> TableNoData([FromQuery] string type) {
            var result = Resolve<IAutoTableService>().TableNoData(type, QueryDictionary().ToJsons(), AutoModel);
            return ToResult(result);
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "获取所有的Api地址")]
        public ApiResult<AutoTable> Table([FromQuery] string type) {
            var result = Resolve<IAutoTableService>().Table(type, QueryDictionary().ToJsons(), AutoModel);
            return ToResult(result);
        }

        /// <summary>
        ///     所有的类型接口，包括枚举、AutoConfig(List对象的)、分类、标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<EnumList>> AllKeyValues() {
            //var result = Resolve<ITypeService>().AllKeyValues();
            //return ApiResult.Success(result.ToList());
            return null;
        }

        ///// <summary>
        ///// 获取UI类型，返回前台组件
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ApiResult<> GetFields(string type)
        //{
        //    var find = type.GetAllPropertys();
        //    if (find == null)
        //    {
        //        return ApiResult.Failure<List<PropertyDescription>>("类型不存在");
        //    }

        //    return ApiResult.Success(find.ToList());
        //}

        /// <summary>
        ///     Get key values
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "根据枚举获取KeyValues")]
        public ApiResult<List<KeyValue>> GetKeyValue([FromQuery] string type) {
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ApiResult.Failure<List<KeyValue>>("类型不能为空");
            }

            if (type == "IAutoTable") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoTableKeyValues());
            }

            if (type == "IAutoForm") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoFormKeyValues());
            }

            if (type == "IAutoList") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoListKeyValues());
            }

            if (type == "IAutoPreview") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoPreviewKeyValues());
            }

            if (type == "IAutoNews") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoNewsKeyValues());
            }

            if (type == "IAutoArticle") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoArticleKeyValues());
            }

            if (type == "IAutoReport") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoReportKeyValues());
            }

            if (type == "IAutoFaq") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoFaqsKeyValues());
            }

            if (type == "IAutoImage") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoImagesKeyValues());
            }

            if (type == "IAutoIndex") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoIndexKeyValues());
            }

            if (type == "IAutoIntro") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoIntroKeyValues());
            }

            if (type == "IAutoTask") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoTaskKeyValues());
            }

            if (type == "IAutoVideo") {
                return ApiResult.Success(Resolve<IAutoUIService>().AutoVideoKeyValues());
            }

            if (type == "CatalogEntity") {
                return ApiResult.Success(Resolve<ITableService>().CatalogEntityKeyValues());
            }

            if (type == "SqlServcieCatalogEntity") {
                return ApiResult.Success(Resolve<ITableService>().SqlServcieCatalogEntityKeyValues());
            }

            if (type == "MongodbCatalogEntity") {
                return ApiResult.Success(Resolve<ITableService>().MongodbCatalogEntityKeyValues());
            }

            var find = type.GetTypeByName();
            if (find == null) {
                return ApiResult.Failure<List<KeyValue>>($"类型不存在，请确认{type}输入是否正确");
            }

            var intances = type.GetInstanceByName();

            if (intances is IRelation) {
                var result = ServiceInterpreter.Eval<IEnumerable<KeyValue>>("IRelationService", "GetKeyValues2", type);
                return ApiResult.Success(result.ToList());
            }

            //enum
            if (find.IsEnum) {
                var keyValues = KeyValueExtesions.EnumToKeyValues(type);
                if (keyValues == null) {
                    return ApiResult.Failure<List<KeyValue>>("枚举不存在");
                }

                return ApiResult.Success(keyValues.ToList());
            }

            // auto config
            if (intances is IAutoConfig) {
                var configValue = Resolve<ITypeService>().GetAutoConfigDictionary(type);
                if (configValue == null) {
                    return ApiResult.Failure<List<KeyValue>>($"{type}不存在");
                }
                var keyValues = new List<KeyValue>();
                foreach (var item in configValue) {
                    keyValues.Add(new KeyValue { Key = item.Key, Value = item.Value });
                }
                return ApiResult.Success(keyValues);
            }
            return null;
        }
    }
}