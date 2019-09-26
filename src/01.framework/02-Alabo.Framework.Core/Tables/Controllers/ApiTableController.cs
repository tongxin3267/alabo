using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;

namespace Alabo.App.Core.Admin.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Table/[action]")]
    public class ApiTableController : ApiBaseController<Table, ObjectId> {

        public ApiTableController() : base() {
            BaseService = Resolve<ITableService>();
        }
    }
}