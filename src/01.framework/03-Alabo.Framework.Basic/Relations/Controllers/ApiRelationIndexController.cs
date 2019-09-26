using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;

namespace Alabo.App.Core.Common.Controllers {

    [ApiExceptionFilter]
    [Route("Api/RelationIndex/[action]")]
    public class ApiRelationIndexController : ApiBaseController<RelationIndex, long> {

        public ApiRelationIndexController() : base() {
            BaseService = Resolve<IRelationIndexService>();
        }
    }
}