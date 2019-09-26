using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Controllers;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;
using Alabo.UI;
using ApiResult = ZKCloud.Open.ApiBase.Models.ApiResult;

namespace Alabo.App.Core.Employes.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Employee/[action]")]
    public class ApiEmployeeController : ApiBaseController<Employee, ObjectId> {
        /// <summary>
        ///     The automatic configuration manager
        /// </summary>

        /// <summary>
        ///     The 会员 manager
        /// </summary>

        public ApiEmployeeController(
            ) : base() {
        }

        /// <summary>
        /// 员工登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<UserOutput> LoginByToken([FromBody]GetLoginToken loginByToken) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var loginInput = Resolve<IEmployeeService>().LoginByToken(loginByToken);
            ApiMemberController apiUserController = new ApiMemberController();
            return apiUserController.Login(loginInput);
        }

        /// <summary>
        /// 员工登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody]UserOutput userOutput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<RoleOuput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<IEmployeeService>().Login(userOutput);
            return ToResult(result);
        }
    }
}