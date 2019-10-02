using Alabo.Data.People.PartnerCompanies.Domain.Entities;
using Alabo.Data.People.PartnerCompanies.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.PartnerCompanies.Controllers {
		[ApiExceptionFilter]
		[Route("Api/PartnerCompany/[action]")]
		public class ApiPartnerCompanyController : ApiBaseController<PartnerCompany,ObjectId>  {
 public ApiPartnerCompanyController() : base() 
	{ 
		BaseService = Resolve<IPartnerCompanyService>();
	}

	}
}
