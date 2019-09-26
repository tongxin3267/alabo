using Alabo.Data.People.Suppliers.Domain.Entities;
using Alabo.Data.People.Suppliers.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.Suppliers.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Supplier/[action]")]
    public class ApiSupplierController : ApiBaseController<Supplier, ObjectId>
    {
        public ApiSupplierController()
        {
            BaseService = Resolve<ISupplierService>();
        }
    }
}