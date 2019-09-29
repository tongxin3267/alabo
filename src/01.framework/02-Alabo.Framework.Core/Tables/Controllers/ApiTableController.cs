using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Tables.Domain.Entities;
using Alabo.Tables.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Framework.Core.Tables.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Table/[action]")]
    public class ApiTableController : ApiBaseController<Table, ObjectId>
    {
        public ApiTableController()
        {
            BaseService = Resolve<ITableService>();
        }
    }
}