using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;

namespace Alabo.App.Core.Common.Controllers {

    [ApiExceptionFilter]
    [Route("Api/StorageFile/[action]")]
    public class ApiStorageFileController : ApiBaseController<StorageFile, ObjectId> {

        public ApiStorageFileController() : base() {
            BaseService = Resolve<IStorageFileService>();
        }
    }
}