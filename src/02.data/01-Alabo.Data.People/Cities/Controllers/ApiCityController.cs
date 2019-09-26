using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.App.Agent.Citys.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using ZKCloud.Open.ApiBase.Models;
using Alabo.UI;

namespace Alabo.App.Agent.Citys.Controllers {

    [ApiExceptionFilter]
    [Route("Api/City/[action]")]
    public class ApiCityController : ApiBaseController<City, ObjectId> {
        
       

        public ApiCityController() : base() {
            BaseService = Resolve<ICityService>();
        }

        /// <summary>
        /// 城市合伙人登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody]UserOutput userOutput) {
            return null;
            ////Todo 城市合伙人登录
            //var result = Resolve<IEmployeeService>().Login(userOutput, () =>
            //{
            //    return (true, FilterType.City);
            //});
            //return Open.ApiBase.Models.ApiResult.Success(result);
        }

        [HttpGet]
        public ApiResult ChangeStatus([FromQuery]string UserId, string Status) {
            if (string.IsNullOrEmpty(UserId)) {
                return ApiResult.Failure("参数传入失败");
            }
            var b = Resolve<ICityService>().ChangeUserStatus(UserId, Status);

            if (b.Succeeded) {
                return ApiResult.Success();
            } else {
                return ApiResult.Failure("操作失败");
            }
        }
    }
}