using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;
using Alabo.UI;

namespace Alabo.App.Market.UserRightss.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Market/[action]")]
    public class ApiMarketController : ApiBaseController {

        public ApiMarketController() : base() {
        }

        /// <summary>
        /// 营销中心登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody]UserOutput userOutput) {
            return null;
            //var result = Resolve<IEmployeeService>().Login(userOutput, () =>
            //{
            //    var isMarker = userOutput.GradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb");
            //    return (isMarker, FilterType.Market);
            //});
            //return ZKCloud.Open.ApiBase.Models.ApiResult.Success(result);
        }
    }
}