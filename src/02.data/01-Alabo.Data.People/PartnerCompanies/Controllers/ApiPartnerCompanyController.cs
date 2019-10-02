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
using Alabo.Data.People.PartnerCompays.Domain.Entities;
using Alabo.Data.People.PartnerCompays.Domain.Services;

namespace Alabo.Data.People.PartnerCompays.Controllers {
		[ApiExceptionFilter]
		[Route("Api/PartnerCompany/[action]")]
		public class ApiPartnerCompanyController : ApiBaseController<PartnerCompany,ObjectId>  {
 public ApiPartnerCompanyController() : base() 
	{ 
		BaseService = Resolve<IPartnerCompanyService>();
	}

	}
}
