using Microsoft.AspNetCore.Mvc;
using System;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.App.Shop.Category.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Category.Controllers {

    [ApiExceptionFilter]
    [Route("Api/CategoryProperty/[action]")]
    public class ApiCategoryPropertyController : ApiBaseController<CategoryProperty, Guid> {

        public ApiCategoryPropertyController() : base() {
            BaseService = Resolve<ICategoryPropertyService>();

        }
    }
}