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
using Alabo.Data.Targets.Reports.Domain.Entities;
using Alabo.Data.Targets.Reports.Domain.Services;

namespace Alabo.Data.Targets.Reports.Controllers {
		[ApiExceptionFilter]
		[Route("Api/TargetReport/[action]")]
		public class ApiTargetReportController : ApiBaseController<TargetReport,long>  {
 public ApiTargetReportController() : base() 
	{ 
		BaseService = Resolve<ITargetReportService>();
	}

	}
}
