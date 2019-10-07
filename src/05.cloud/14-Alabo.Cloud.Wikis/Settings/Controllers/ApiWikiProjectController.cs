using Alabo.Cloud.Wikis.Settings.Domain.Entities;
using Alabo.Cloud.Wikis.Settings.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.Wikis.Settings.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/WikiProject/[action]")]
    public class ApiWikiProjectController : ApiBaseController<WikiProject, ObjectId>
    {
        public ApiWikiProjectController() : base() {
            BaseService = Resolve<IWikiProjectService>();
        }
    }
}