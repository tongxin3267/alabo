using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.People.UserQrCode.Domain.Services;
using Alabo.Cloud.People.UserQrCode.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.UserQrCode.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Qrcode/[action]")]
    public class ApiQrcodeController : ApiBaseController
    {
        [HttpGet]
        [Display(Description = "用户二维码")]
        public ApiResult<PageResult<ViewImagePage>> QrcodeList([FromQuery] PagedInputDto parameter)
        {
            var model = Resolve<IUserQrCodeService>().GetQrCodeList(parameter);

            var apiRusult = new PageResult<ViewImagePage>
            {
                PageCount = model.PageCount,
                Result = model,
                RecordCount = model.RecordCount,
                CurrentSize = model.CurrentSize,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };

            return ApiResult.Success(apiRusult);
        }

        /// <summary>
        ///     二维码
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "二维码")]
        [ApiAuth]
        public ApiResult<string> QrCode([FromQuery] long loginUserId)
        {
            var result = Resolve<IUserQrCodeService>().QrCore(loginUserId);
            if (result == null) return ApiResult.Failure<string>("用户不存在或者已经删除！");

            return ApiResult.Success<string>(result);
        }
    }
}