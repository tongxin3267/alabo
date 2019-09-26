using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Alabo.Industry.Shop.Products.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Industry.Shop.Products.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ProductDetail/[action]")]
    public class ApiProductDetailController : ApiBaseController<ProductDetail, long> {

        public ApiProductDetailController() : base() {
            BaseService = Resolve<IProductDetailService>();

        }
    }
}