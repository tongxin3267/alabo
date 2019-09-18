using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.App.Core.UserType.Domain.Services;

namespace Alabo.App.Core.UserType.Controllers {

    [ApiExceptionFilter]
    [Route("Api/TypeGrade/[action]")]
    public class ApiTypeGradeController : ApiBaseController<TypeGrade, ObjectId> {

        public ApiTypeGradeController() : base() {
            BaseService = Resolve<ITypeGradeService>();
        }
    }
}