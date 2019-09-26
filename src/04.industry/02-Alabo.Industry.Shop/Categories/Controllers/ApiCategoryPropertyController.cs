using System;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Categories.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Industry.Shop.Categories.Controllers {

    [ApiExceptionFilter]
    [Route("Api/CategoryProperty/[action]")]
    public class ApiCategoryPropertyController : ApiBaseController<CategoryProperty, Guid> {

        public ApiCategoryPropertyController() : base() {
            BaseService = Resolve<ICategoryPropertyService>();

        }
    }
}