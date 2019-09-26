using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Data.Things.Goodss.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Goods/[action]")]
    public class ApiGoodsController : ApiBaseController<Goods, long>
    {
        public ApiGoodsController()
        {
            BaseService = Resolve<IGoodsService>();
        }
    }
}