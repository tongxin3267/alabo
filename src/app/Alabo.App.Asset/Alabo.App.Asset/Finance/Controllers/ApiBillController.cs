using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Finance.ViewModels.Bill;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;
using ApiResult = ZKCloud.Open.ApiBase.Models.ApiResult;

namespace Alabo.App.Core.Finance.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Bill/[action]")]
    public class ApiBillController : ApiBaseController<Bill, long> {

        public ApiBillController() : base() {
            BaseService = Resolve<IBillService>();
        }

        [HttpGet]
        [Display(Description = "²ÆÎñÃ÷Ï¸")]
        public ApiResult<PageResult<ViewAdminBill>> ViewBillList([FromQuery] PagedInputDto parameter, object query) {
            var list = Resolve<IFinanceAdminService>().GetBillPageList(Query);
            PageResult<ViewAdminBill> apiRusult = new PageResult<ViewAdminBill> {
                PageCount = list.PageCount,
                Result = list,
                RecordCount = list.RecordCount,
                CurrentSize = list.CurrentSize,
                PageIndex = list.PageIndex,
                PageSize = list.PageSize,
            };

            if (list.Count < 0) {
                return ApiResult.Success(new PageResult<ViewAdminBill>());
            }
            return ApiResult.Success(apiRusult);
        }
    }
}