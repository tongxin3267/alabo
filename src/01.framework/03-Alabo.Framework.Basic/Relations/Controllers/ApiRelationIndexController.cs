using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Framework.Basic.Relations.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/RelationIndex/[action]")]
    public class ApiRelationIndexController : ApiBaseController<RelationIndex, long>
    {
        public ApiRelationIndexController()
        {
            BaseService = Resolve<IRelationIndexService>();
        }
    }
}