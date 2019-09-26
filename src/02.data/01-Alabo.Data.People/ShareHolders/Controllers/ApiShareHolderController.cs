using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Agent.ShareHolders.Domain.Entities;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.App.Agent.ShareHolders.Domain.Services;

namespace Alabo.App.Agent.ShareHolders.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ShareHolder/[action]")]
    public class ApiShareHolderController : ApiBaseController<ShareHolder, ObjectId> {
        
       

        public ApiShareHolderController() : base() {
            BaseService = Resolve<IShareHolderService>();
       
        }
    }
}