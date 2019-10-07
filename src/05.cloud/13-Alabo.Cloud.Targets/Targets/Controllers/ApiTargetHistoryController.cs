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
using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Data.Targets.Targets.Domain.Services;

namespace Alabo.Data.Targets.Targets.Controllers {
		[ApiExceptionFilter]
		[Route("Api/TargetHistory/[action]")]
		public class ApiTargetHistoryController : ApiBaseController<TargetHistory,ObjectId>  {
 public ApiTargetHistoryController() : base() 
	{ 
		BaseService = Resolve<ITargetHistoryService>();
	}

	}
}