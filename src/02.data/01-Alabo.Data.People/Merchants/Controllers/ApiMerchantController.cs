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
using Alabo.Data.People.Merchants.Domain.Entities;
using Alabo.Data.People.Merchants.Domain.Services;

namespace Alabo.Data.People.Merchants.Controllers {
		[ApiExceptionFilter]
		[Route("Api/Merchant/[action]")]
		public class ApiMerchantController : ApiBaseController<Merchant,ObjectId>  {
 public ApiMerchantController() : base() 
	{ 
		BaseService = Resolve<IMerchantService>();
	}

	}
}
