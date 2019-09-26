using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.ViewModels;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Qrcode/[action]")]
    public class ApiQrcodeController : ApiBaseController {

        public ApiQrcodeController() : base() {
        }

        [HttpGet]
        [Display(Description = "用户二维码")]
        public ApiResult<PageResult<ViewImagePage>> QrcodeList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IUserQrCodeService>().GetQrCodeList(parameter);

            PageResult<ViewImagePage> apiRusult = new PageResult<ViewImagePage> {
                PageCount = model.PageCount,
                Result = model,
                RecordCount = model.RecordCount,
                CurrentSize = model.CurrentSize,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
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
        public ApiResult<string> QrCode([FromQuery] long loginUserId) {
            var result = Resolve<IUserQrCodeService>().QrCore(loginUserId);
            if (result == null) {
                return ApiResult.Failure<string>("用户不存在或者已经删除！");
            }

            return ApiResult.Success<string>(result);
        }
    }
}