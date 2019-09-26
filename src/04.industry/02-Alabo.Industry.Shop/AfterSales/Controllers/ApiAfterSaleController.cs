using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Controllers {

    [ApiExceptionFilter]
    [Route("Api/AfterSale/[action]")]
    public class ApiAfterSaleController : ApiBaseController<Domain.Entities.AfterSale, ObjectId> {
        
       

        public ApiAfterSaleController() : base() {
       
        }
    }
}