using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserAddress/[action]")]
    public class ApiUserAddressController : ApiBaseController<UserAddress, ObjectId> {

        public ApiUserAddressController() {
            BaseService = Resolve<IUserAddressService>();
        }
    }
}