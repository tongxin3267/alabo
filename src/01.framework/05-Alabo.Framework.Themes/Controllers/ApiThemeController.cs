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
        ///     ����Ĭ��ģ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SetDefaultTheme(string id)
        {
            if (Resolve<IThemeService>().SetDefaultTheme(id.ToObjectId())) {
                return ApiResult.Success();
            }

            return ApiResult.Failure("Ĭ��ģ������ʧ��");
        }

        [HttpGet]
        public ApiResult GetTheme(string id)
        {
            Resolve<IThemeService>().InitTheme(id);

            return ApiResult.Success();
        }

        /// <summary>
        ///     ��ȡ����ҳ������
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "��ȡ����ҳ������")]
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