using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebUis.Controllers
{
    /// <summary>
    ///     控制类型
    /// </summary>
    [Route("Api/Field/[action]")]
    public class ApiFieldController : ApiBaseController
    {
        /// <summary>
        ///     获取可统计的字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<KeyValue>> ReportField(string type)
        {
            var find = type.GetAllPropertys();
            if (find == null) return ApiResult.Failure<List<KeyValue>>("类型不存在");

            var list = new List<KeyValue>();

            foreach (var item in find)
                if (item.Property.PropertyType == typeof(decimal) || item.Property.PropertyType == typeof(int)
                                                                  || item.Property.PropertyType == typeof(long)
                                                                  || item.Property.PropertyType == typeof(short))
                {
                    if (item.Property.Name == "Id") continue;
                    var key = new KeyValue
                    {
                        Name = $"{item.DisplayAttribute.Name}({item.Property.Name})",
                        Value = item.Property.Name,
                        Key = item.Property.Name
                    };

                    list.Add(key);
                }

            return ApiResult.Success(list);
        }

        /// <summary>
        ///     获取字段所有的字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<KeyValue>> All(string type)
        {
            var find = type.GetAllPropertys();
            if (find == null) return ApiResult.Failure<List<KeyValue>>("类型不存在");

            var list = new List<KeyValue>();

            foreach (var item in find)
            {
                var key = new KeyValue
                {
                    Name = $"{item.DisplayAttribute.Name}({item.Property.Name})",
                    Value = item.Property.Name,
                    Key = item.Property.Name
                };

                list.Add(key);
            }

            return ApiResult.Success(list);
        }

        /// <summary>
        ///     获取可条件选择的字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<KeyValue>> QueryField(string type)
        {
            var find = type.GetAllPropertys();
            if (find == null) return ApiResult.Failure<List<KeyValue>>("类型不存在");

            var list = new List<KeyValue>();

            foreach (var item in find)
                if (item.Property.PropertyType == typeof(decimal)
                    || item.Property.PropertyType == typeof(Enum)
                    || item.Property.PropertyType.BaseType == typeof(Enum)
                    || item.Property.PropertyType == typeof(int)
                    || item.Property.PropertyType == typeof(long)
                    || item.Property.PropertyType == typeof(short))
                {
                    if (item.Property.Name == "Id") continue;
                    var key = new KeyValue
                    {
                        Name = $"{item.DisplayAttribute.Name}({item.Property.Name})",
                        Value = item.Property.Name,
                        Key = item.Property.Name
                    };

                    list.Add(key);
                }

            return ApiResult.Success(list);
        }
    }
}