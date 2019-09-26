using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Data.Things.Brands.Domain.Entities;
using Alabo.Data.Things.Brands.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.Things.Brands.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Brand/[action]")]
    public class ApiBrandController : ApiBaseController<Brand, ObjectId> {

        public ApiBrandController() : base() {
            BaseService = Resolve<IBrandService>();
        }
    }
}