using Alabo.Data.People.Provinces.Domain.Entities;
using Alabo.Data.People.Provinces.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Province/[action]")]
    public class ApiProvinceController : ApiBaseController<Province, ObjectId>
    {
        public ApiProvinceController()
        {
            BaseService = Resolve<IProvinceService>();
        }
    }
}