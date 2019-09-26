﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using UserDetail = Alabo.Users.Entities.UserDetail;

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
    }
}