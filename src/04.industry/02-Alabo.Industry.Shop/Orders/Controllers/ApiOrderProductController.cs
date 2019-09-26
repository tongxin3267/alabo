using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;

using Alabo.App.Core.User;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Order.Controllers {

    [ApiExceptionFilter]
    [Route("Api/OrderProduct/[action]")]
    public class ApiOrderProductController : ApiBaseController<OrderProduct, long> {

        public ApiOrderProductController() : base() {
            BaseService = Resolve<IOrderProductService>();

        }
    }
}