using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.RestfulApi;using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.Cloud.Wikis.Wikis.Domain.Entities;
using Alabo.Cloud.Wikis.Wikis.Domain.Services;

namespace Alabo.Cloud.Wikis.Wikis.Controllers {
		[ApiExceptionFilter]
		[Route("Api/WikiHistory/[action]")]
		public class ApiWikiHistoryController : ApiBaseController<WikiHistory,ObjectId>  {
 public ApiWikiHistoryController() : base() 
	{ 
		BaseService = Resolve<IWikiHistoryService>();
	}

	}
}
