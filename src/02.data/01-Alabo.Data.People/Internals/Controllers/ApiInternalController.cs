using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Agent.Internal.Domain.Entities;
using Alabo.App.Agent.Internal.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;

namespace Alabo.App.Agent.Internal.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Internal/[action]")]
    public class ApiInternalController : ApiBaseController<ParentInternal, ObjectId> {

        public ApiInternalController() : base() {
            BaseService = Resolve<IParentInternalService>();
        }
    }
}