using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
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