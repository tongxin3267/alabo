using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Employes.Domain.Services;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Controllers;
using Alabo.Data.People.Users.Dtos;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Helpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.People.Employes.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Employee/[action]")]
    public class ApiEmployeeController : ApiBaseController<Employee, ObjectId>
    {
        /// <summary>
        ///     Ա����¼
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<UserOutput> LoginByToken([FromBody] GetLoginToken loginByToken) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<UserOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            if (loginByToken.Tenant.IsNullOrEmpty()) {
                ApiResult.Failure<UserOutput>("租户不能为空");
            }
            if (loginByToken.Tenant != HttpWeb.Tenant) {
                ApiResult.Failure<UserOutput>("租户传入错误");
            }

            var loginInput = Resolve<IEmployeeService>().LoginByToken(loginByToken);
            var apiUserController = new ApiMemberController();
            return apiUserController.Login(loginInput);
        }

        /// <summary>
        ///     Ա����¼
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody] UserOutput userOutput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<RoleOuput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IEmployeeService>().Login(userOutput);
            return ToResult(result);
        }
    }
}