using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.App.Core.Common.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/RelationIndex/[action]")]
    public class ApiRelationIndexController : ApiBaseController<RelationIndex, long> {

        public ApiRelationIndexController() : base() {
            BaseService = Resolve<IRelationIndexService>();
        }
    }
}