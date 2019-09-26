using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;

using Alabo.App.Core.User;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Product.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ProductDetail/[action]")]
    public class ApiProductDetailController : ApiBaseController<ProductDetail, long> {

        public ApiProductDetailController() : base() {
            BaseService = Resolve<IProductDetailService>();

        }
    }
}