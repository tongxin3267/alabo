using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Core.Tables.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Table/[action]")]
    public class ApiTableController : ApiBaseController<Table, ObjectId> {

        public ApiTableController() : base() {
            BaseService = Resolve<ITableService>();
        }
    }
}