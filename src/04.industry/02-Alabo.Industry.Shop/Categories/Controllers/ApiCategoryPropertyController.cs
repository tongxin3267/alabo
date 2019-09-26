using Microsoft.AspNetCore.Mvc;
using System;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
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