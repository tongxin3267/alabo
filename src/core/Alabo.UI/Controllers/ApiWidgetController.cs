﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.UI.Widgets;

namespace Alabo.App.Core.Common.Controllers {

    /// <summary>
    /// 对应前端的Widget模块
    /// </summary>
    [Route("Api/Widget/[action]")]
    public class ApiWidgetController : ApiBaseController {

        public ApiWidgetController() : base() {
        }

        /// <summary>
        ///     根据配置获取自动表单的值
        /// </summary>
        [HttpGet]
        [Display(Description = "根据配置获取自动表单的值")]
        public ApiResult<object> Get([FromQuery]WidgetInput input) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<object>(this.FormInvalidReason());
            }

            var type = input.Type.GetTypeByName();
            if (type == null) {
                return ApiResult.Failure<object>($"类型{input.Type}不存在");
            }

            if (!type.FullName.EndsWith("Widget") || !type.FullName.Contains("Widgets") || !type.FullName.Contains("UI")) {
                return ApiResult.Failure<object>($"类型{input.Type}Widget的命名约定，不符合约定");
            }
            var find = input.Type.GetInstanceByName();
            if (find == null) {
                return ApiResult.Failure<object>($"类型{input.Type}不存在");
            }

            if (find is IWidget set) {
                var result = set.Get(input.Json);
                return ApiResult.Success(result);
            } else {
                return ApiResult.Failure<object>($"非IWidget类型，请确认{input.Type}输入是否正确");
            }
        }
    }
}