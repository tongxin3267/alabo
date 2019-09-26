using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Cms.Support.Domain.Dtos;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.App.Cms.Support.Domain.Enum;
using Alabo.App.Cms.Support.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;

using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Support.Controllers {

    [ApiExceptionFilter]
    [Route("Api/WorkOrder/[action]")]
    public class ApiWorkOrderController : ApiBaseController<WorkOrder, ObjectId> {
        

        public ApiWorkOrderController() : base() {
            
            BaseService = Resolve<IWorkOrderService>();
        }

        [HttpPost]
        public ApiResult Add([FromBody] WorkOrderInput workOrder) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var user = Resolve<IUserService>().GetSingle(u => u.Id == workOrder.LoginUserId);
            if (user == null) {
                return ApiResult.Failure("会员为空");
            }
            var view = new WorkOrder {
                Description = workOrder.Description,
                ClassId = WorkOrderType.Problem.Value(),
                Title = $"用户[{user.UserName}]问题反馈",
                Type = WorkOrderType.Problem,
                UserId = workOrder.LoginUserId,
                State = WorkOrderState.Accept
            };
            Resolve<IWorkOrderService>().AddWorkOrder(view);

            return ApiResult.Success();
        }

        [HttpGet]
        [Display(Description = "工单系统")]
        public ApiResult<PagedList<WorkOrder>> WordOrderList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IWorkOrderServices>().GetPagedList(Query);
            return ApiResult.Success(model);
        }
    }
}