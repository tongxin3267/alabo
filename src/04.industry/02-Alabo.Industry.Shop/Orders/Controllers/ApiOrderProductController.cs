using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Industry.Shop.Orders.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/OrderProduct/[action]")]
    public class ApiOrderProductController : ApiBaseController<OrderProduct, long>
    {
        public ApiOrderProductController()
        {
            BaseService = Resolve<IOrderProductService>();
        }
    }
}