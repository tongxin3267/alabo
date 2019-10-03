using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Domain.Services;
using Alabo.Framework.Themes.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Themes.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Theme/[action]")]
    public class ApiThemeController : ApiBaseController<Theme, ObjectId>
    {
        public ApiThemeController()
        {
            BaseService = Resolve<IThemeService>();
        }

        /// <summary>
        ///     设置默认模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SetDefaultTheme(string id)
        {
            if (Resolve<IThemeService>().SetDefaultTheme(id.ToObjectId())) {
                return ApiResult.Success();
            }

            return ApiResult.Failure("默认模板设置失败");
        }

        [HttpGet]
        public ApiResult GetTheme(string id)
        {
            Resolve<IThemeService>().InitTheme(id);

            return ApiResult.Success();
        }

        /// <summary>
        ///     获取所有页面配置
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "获取所有页面配置")]
        [AllowAnonymous]
        public ApiResult<AllClientPages> GetAllClientPages([FromQuery] ClientPageInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<AllClientPages>(this.FormInvalidReason());
            }

            try
            {
                var allClientPages = Resolve<IThemePageService>().GetAllClientPages(parameter);
                allClientPages.LastUpdate = DateTime.Now.AddMinutes(10).ConvertDateTimeInt();
                return ApiResult.Success(allClientPages);
            }
            catch (Exception ex)
            {
                return ApiResult.Failure<AllClientPages>(ex.Message);
            }
        }
    }
}