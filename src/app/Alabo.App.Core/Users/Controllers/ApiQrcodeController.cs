using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.ViewModels;
using Alabo.App.Core.User.Domain.Services;
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
    }
}