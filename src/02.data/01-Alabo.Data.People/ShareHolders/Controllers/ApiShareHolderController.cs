using Alabo.Data.People.ShareHolders.Domain.Entities;
using Alabo.Data.People.ShareHolders.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.ShareHolders.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ShareHolder/[action]")]
    public class ApiShareHolderController : ApiBaseController<ShareHolder, ObjectId> {
        
       

        public ApiShareHolderController() : base() {
            BaseService = Resolve<IShareHolderService>();
       
        }
    }
}