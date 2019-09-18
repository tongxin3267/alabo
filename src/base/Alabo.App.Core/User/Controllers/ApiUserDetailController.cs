using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using UserDetail = Alabo.App.Core.User.Domain.Entities.UserDetail;

namespace Alabo.App.Core.User.Controllers {

    /// <summary>
    ///     用户相关Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/UserDetail/[action]")]
    public class ApiUserDetailController : ApiBaseController<UserDetail, long> {

        /// <summary>
        /// </summary>
        public ApiUserDetailController(
            ) : base() {
            BaseService = Resolve<IUserDetailService>();
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

        /// <summary>
        ///     组织架构图函数
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "组织架构图函数")]
        [ApiAuth]
        public ApiResult Tree(long loginUserId) {
            //var ll = Resolve<IUserService>().GetList(e => e.ParentId == loginUserId);
            var bRoot = loginUserId == 0;
            if (bRoot) {
                loginUserId = AutoModel.BasicUser.Id;
            }

            var userTreeList = Resolve<IUserTreeService>().GetUserTree(loginUserId);
            var kk = userTreeList.ToJson();
            return ApiResult.Success(userTreeList);
        }
    }
}