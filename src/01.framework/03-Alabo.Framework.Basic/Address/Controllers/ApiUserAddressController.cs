using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using MongoDB.Bson;

namespace Alabo.App.Core.Common.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserAddress/[action]")]
    public class ApiUserAddressController : ApiBaseController<UserAddress, ObjectId> {

        public ApiUserAddressController() : base() {
            BaseService = Resolve<IUserAddressService>();
        }
    }
}