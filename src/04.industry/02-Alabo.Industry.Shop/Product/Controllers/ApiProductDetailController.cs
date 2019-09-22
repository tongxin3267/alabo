using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
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