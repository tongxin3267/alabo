using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Alabo.Users.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Grade/[action]")]
    public class ApiGradeController : ApiBaseController {
        /// <summary>
        ///     The automatic configuration manager
        /// </summary>

        /// <summary>
        ///     The message manager
        /// </summary>

        /// <summary>
        ///     The 会员 manager
        /// </summary>

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiUserController" /> class.
        /// </summary>
        public ApiGradeController(
            ) : base() {
        }

        /// <summary>
        ///     会员的所有等级
        /// </summary>
        [HttpGet]
        [Display(Description = "所有等级")]
        public ApiResult<List<UserGradeConfig>> AllGrade() {
            var grades = Resolve<IGradeService>().GetUserGradeList().Where(g => g.Visible).ToList()
                .OrderBy(g => g.SortOrder).ToList();
            return ApiResult.Success(grades);
        }

        /// <summary>
        ///     获取比自己级别低的等级（包括自己等级）
        ///     第一个显示未启用会员
        /// </summary>
        [HttpGet]
        [Display(Description = "获取比自己级别低的等级（包括自己等级）")]
        public ApiResult<List<UserGradeConfig>> GetLowGrade([FromQuery]long loginUserId) {
            return null;
        }

        /// <summary>
        ///     根据用户名获取会员等级
        /// </summary>
        /// <param name="userName">用户名</param>
        [HttpGet]
        [Display(Description = "根据用户名获取会员等级")]
        public ApiResult<UserGradeConfig> GetUserGrade([FromQuery]string userName) {
            var user = Resolve<IAlaboUserService>().GetSingle(userName);
            if (user == null) {
                return ApiResult.Failure<UserGradeConfig>("用户不存在");
            }

            var grade = Resolve<IGradeService>().GetGrade(user.GradeId);
            return ApiResult.Success(grade);
        }

        /// <summary>
        ///     修改会员等级
        /// </summary>
        [HttpPost]
        [Display(Description = "修改会员等级")]
        public ApiResult UpdateGrade([FromBody] UserGradeInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IGradeService>().UpdateUserGrade(parameter);
            return ToResult(result);
        }
    }
}