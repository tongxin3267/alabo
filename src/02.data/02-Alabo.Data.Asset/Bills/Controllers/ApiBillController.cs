using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Services;
using Alabo.App.Asset.Bills.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis.Models.Lists;
using Alabo.Framework.Core.WebUis.Models.Previews;
using Alabo.Framework.Themes.Extensions;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.Bills.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Bill/[action]")]
    public class ApiBillController : ApiBaseController<Bill, long>
    {
        public ApiBillController()
        {
            BaseService = Resolve<IBillService>();
        }

        /// <summary>
        ///     Bills the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "bill  the  soecified parameter")]
        [ApiAuth]
        public ApiResult<ListOutput> Bill([FromQuery] ListInput parameter)
        {
            var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes();
            var billApiInput = new BillApiInput
            {
                LoginUserId = parameter.LoginUserId,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize
            };

            var billList = Resolve<IFinanceAdminService>().GetApiBillPageList(billApiInput, out var count);
            var result = new ListOutput
            {
                StyleType = 1, //采用第一种样式
                TotalSize = count / parameter.PageSize
            };
            foreach (var item in billList.ToList())
            {
                var index = new Random().Next(1, 8);
                var apiData = new ListItem
                {
                    Id = item.Id,
                    Intro = $"账后{item.AfterAmount} 时间{item.CreateTime.ToString("yyyy-MM-dd hh:ss")}",
                    Title =
                        $"{moneyTypes.FirstOrDefault(e => e.Id == item.MoneyTypeId)?.Name}-{item.Type.GetDisplayName()}",
                    Image = Resolve<IApiService>().ApiImageUrl($"/assets/mobile/images/icon/demo{index}.png"),
                    Url = $"/finance/bill/view?id={item.Id}".ToClientUrl(ClientType.WapH5),
                    Extra = item.Amount.ToStr()
                };
                result.ApiDataList.Add(apiData);
            }

            return ApiResult.Success(result);
        }

        /// <summary>
        ///     Bills the 视图.
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "bills the 视图 ")]
        public ApiResult BillView([FromQuery] PreviewInput parameter)
        {
            var view = Resolve<IFinanceAdminService>().GetViewBillSingle(parameter.Id.ConvertToLong());
            if (view == null) return ApiResult.Failure("账单不存在或者已经删除！");

            if (view.Bill.UserId != parameter.LoginUserId) return ApiResult.Failure("您无权查看该账单！");

            var apiOutput = Resolve<IFinanceAdminService>().GetBillOutput(view);
            var temp = apiOutput.ToKeyValues();

            return ApiResult.Success(temp);
        }

        [HttpGet]
        [Display(Description = "财务明细")]
        public ApiResult<PageResult<ViewAdminBill>> ViewBillList([FromQuery] PagedInputDto parameter, object query)
        {
            var list = Resolve<IFinanceAdminService>().GetBillPageList(Query);
            var apiRusult = new PageResult<ViewAdminBill>
            {
                PageCount = list.PageCount,
                Result = list,
                RecordCount = list.RecordCount,
                CurrentSize = list.CurrentSize,
                PageIndex = list.PageIndex,
                PageSize = list.PageSize
            };

            if (list.Count < 0) return ApiResult.Success(new PageResult<ViewAdminBill>());
            return ApiResult.Success(apiRusult);
        }
    }
}