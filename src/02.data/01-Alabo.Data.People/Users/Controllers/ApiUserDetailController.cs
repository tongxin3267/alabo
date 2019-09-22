﻿using Microsoft.AspNetCore.Mvc;
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
    }
}